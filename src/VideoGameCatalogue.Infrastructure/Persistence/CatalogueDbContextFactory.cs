using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace VideoGameCatalogue.Infrastructure.Persistence;

/// <summary>
/// Used only by the EF Core tooling (e.g. <c>dotnet ef migrations add</c>) so migrations can be
/// generated from this project without booting the API. Not used at runtime.
/// </summary>
public sealed class CatalogueDbContextFactory : IDesignTimeDbContextFactory<CatalogueDbContext>
{
    public CatalogueDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<CatalogueDbContext>()
            .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=VideoGameCatalogue;Trusted_Connection=True;")
            .Options;

        return new CatalogueDbContext(options);
    }
}
