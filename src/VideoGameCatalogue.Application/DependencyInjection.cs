using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VideoGameCatalogue.Application.VideoGames;

namespace VideoGameCatalogue.Application;

/// <summary>
/// Registers the Application layer's services. Keeping the wiring next to the layer it
/// configures lets the API compose the app without knowing each layer's internals.
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection));
        services.AddScoped<IVideoGameService, VideoGameService>();

        return services;
    }
}
