using VideoGameCatalogue.Domain.Entities;

namespace VideoGameCatalogue.Application.Abstractions.Persistence;

/// <summary>
/// Persistence contract for the <see cref="VideoGame"/> aggregate. Declared in the Application
/// layer and implemented by Infrastructure so dependencies point inward.
/// </summary>
public interface IVideoGameRepository
{
    Task<IReadOnlyList<VideoGame>> GetAllAsync(string? search, CancellationToken cancellationToken = default);

    Task<VideoGame?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
