using Microsoft.EntityFrameworkCore;
using VideoGameCatalogue.Application.Abstractions.Persistence;
using VideoGameCatalogue.Domain.Entities;

namespace VideoGameCatalogue.Infrastructure.Persistence.Repositories;

internal sealed class VideoGameRepository : IVideoGameRepository
{
    private readonly CatalogueDbContext _context;

    public VideoGameRepository(CatalogueDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<VideoGame>> GetAllAsync(string? search, CancellationToken cancellationToken = default)
    {
        var query = _context.Games.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim();
            query = query.Where(g =>
                g.Title.Contains(term) ||
                g.Genre.Contains(term) ||
                g.Platform.Contains(term) ||
                g.Publisher.Contains(term));
        }

        return await query
            .OrderBy(g => g.Title)
            .ToListAsync(cancellationToken);
    }

    public Task<VideoGame?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _context.Games.FirstOrDefaultAsync(g => g.Id == id, cancellationToken);

    public Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        _context.SaveChangesAsync(cancellationToken);
}
