using Microsoft.EntityFrameworkCore;
using VideoGameCatalogue.Domain.Entities;

namespace VideoGameCatalogue.Infrastructure.Persistence;

public sealed class CatalogueDbContext : DbContext
{
    public CatalogueDbContext(DbContextOptions<CatalogueDbContext> options) : base(options)
    {
    }

    public DbSet<VideoGame> Games => Set<VideoGame>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogueDbContext).Assembly);
    }
}
