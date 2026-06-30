using VideoGameCatalogue.Application.VideoGames.Dtos;
using VideoGameCatalogue.Domain.Entities;

namespace VideoGameCatalogue.Application.VideoGames.Mapping;

/// <summary>
/// Manual mapping between the domain entity and its DTO. Explicit and trivially debuggable;
/// a mapping library would add a dependency without earning it for a model this small.
/// </summary>
internal static class VideoGameMappings
{
    public static VideoGameDto ToDto(this VideoGame game) => new(
        game.Id,
        game.Title,
        game.Genre,
        game.Platform,
        game.Publisher,
        game.ReleaseDate,
        game.Rating,
        game.Description);
}
