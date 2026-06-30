using VideoGameCatalogue.Application.VideoGames.Dtos;

namespace VideoGameCatalogue.Application.VideoGames;

/// <summary>
/// Application boundary for the catalogue use cases. Consumed by the API and mocked in tests.
/// </summary>
public interface IVideoGameService
{
    Task<IReadOnlyList<VideoGameDto>> GetAllAsync(string? search, CancellationToken cancellationToken = default);

    Task<VideoGameDto> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<VideoGameDto> UpdateAsync(int id, UpdateVideoGameRequest request, CancellationToken cancellationToken = default);
}
