using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoGameCatalogue.Domain.Entities;

namespace VideoGameCatalogue.Infrastructure.Persistence.Configurations;

internal sealed class VideoGameConfiguration : IEntityTypeConfiguration<VideoGame>
{
    public void Configure(EntityTypeBuilder<VideoGame> builder)
    {
        builder.ToTable("VideoGames");

        builder.HasKey(g => g.Id);

        builder.Property(g => g.Title)
            .HasMaxLength(VideoGame.TitleMaxLength)
            .IsRequired();

        builder.Property(g => g.Genre)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(g => g.Platform)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(g => g.Publisher)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(g => g.ReleaseDate)
            .HasColumnType("date");

        builder.Property(g => g.Rating)
            .HasPrecision(3, 1);

        builder.Property(g => g.Description)
            .HasMaxLength(2000);

        builder.HasIndex(g => g.Title);

        builder.HasData(VideoGameSeed.Games);
    }
}
