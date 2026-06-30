using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VideoGameCatalogue.Application.Abstractions.Persistence;
using VideoGameCatalogue.Infrastructure.Persistence;
using VideoGameCatalogue.Infrastructure.Persistence.Repositories;

namespace VideoGameCatalogue.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Catalogue")
            ?? throw new InvalidOperationException("Connection string 'Catalogue' was not configured.");

        services.AddDbContext<CatalogueDbContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped<IVideoGameRepository, VideoGameRepository>();

        return services;
    }
}
