using Microsoft.EntityFrameworkCore;
using VideoGameCatalogue.API.Middleware;
using VideoGameCatalogue.Application;
using VideoGameCatalogue.Infrastructure;
using VideoGameCatalogue.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

const string AngularCorsPolicy = "AngularClient";

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy(AngularCorsPolicy, policy =>
        policy.WithOrigins(builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [])
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    await app.MigrateDatabaseAsync();
}

app.UseCors(AngularCorsPolicy);
app.MapControllers();

app.Run();

internal static class WebApplicationExtensions
{
    /// <summary>Applies pending migrations on startup so a fresh clone runs without manual EF commands.</summary>
    public static async Task MigrateDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CatalogueDbContext>();
        await context.Database.MigrateAsync();
    }
}
