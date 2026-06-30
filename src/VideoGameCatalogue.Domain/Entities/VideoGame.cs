using VideoGameCatalogue.Domain.Exceptions;

namespace VideoGameCatalogue.Domain.Entities;

/// <summary>
/// Aggregate root of the catalogue. State changes flow through the constructor or
/// <see cref="Update"/> so the entity can never be persisted in an invalid state.
/// </summary>
public sealed class VideoGame
{
    public const decimal MinRating = 0m;
    public const decimal MaxRating = 10m;
    public const int TitleMaxLength = 200;

    public int Id { get; private set; }
    public string Title { get; private set; } = null!;
    public string Genre { get; private set; } = null!;
    public string Platform { get; private set; } = null!;
    public string Publisher { get; private set; } = null!;
    public DateOnly ReleaseDate { get; private set; }
    public decimal Rating { get; private set; }
    public string? Description { get; private set; }

    // Required by EF Core's materialization; never used by application code.
    private VideoGame()
    {
    }

    public VideoGame(
        string title,
        string genre,
        string platform,
        string publisher,
        DateOnly releaseDate,
        decimal rating,
        string? description = null)
    {
        Apply(title, genre, platform, publisher, releaseDate, rating, description);
    }

    public void Update(
        string title,
        string genre,
        string platform,
        string publisher,
        DateOnly releaseDate,
        decimal rating,
        string? description = null)
    {
        Apply(title, genre, platform, publisher, releaseDate, rating, description);
    }

    private void Apply(
        string title,
        string genre,
        string platform,
        string publisher,
        DateOnly releaseDate,
        decimal rating,
        string? description)
    {
        var trimmedTitle = Required(title, nameof(Title));
        if (trimmedTitle.Length > TitleMaxLength)
        {
            throw new DomainValidationException($"{nameof(Title)} must not exceed {TitleMaxLength} characters.");
        }

        Title = trimmedTitle;
        Genre = Required(genre, nameof(Genre));
        Platform = Required(platform, nameof(Platform));
        Publisher = Required(publisher, nameof(Publisher));

        if (rating < MinRating || rating > MaxRating)
        {
            throw new DomainValidationException($"Rating must be between {MinRating} and {MaxRating}.");
        }

        ReleaseDate = releaseDate;
        Rating = rating;
        Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim();
    }

    private static string Required(string value, string field)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainValidationException($"{field} is required.");
        }

        return value.Trim();
    }
}
