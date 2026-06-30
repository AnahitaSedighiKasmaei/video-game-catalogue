using FluentValidation.TestHelper;
using VideoGameCatalogue.Application.VideoGames.Dtos;
using VideoGameCatalogue.Application.VideoGames.Validation;

namespace VideoGameCatalogue.UnitTests.Application;

public class UpdateVideoGameRequestValidatorTests
{
    private readonly UpdateVideoGameRequestValidator _validator = new();

    private static UpdateVideoGameRequest Valid() => new(
        Title: "Celeste",
        Genre: "Platformer",
        Platform: "PC",
        Publisher: "Maddy Makes Games",
        ReleaseDate: new DateOnly(2018, 1, 25),
        Rating: 9.1m,
        Description: null);

    [Fact]
    public void ValidRequest_PassesValidation()
    {
        var result = _validator.TestValidate(Valid());

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void EmptyTitle_FailsValidation()
    {
        var result = _validator.TestValidate(Valid() with { Title = "" });

        result.ShouldHaveValidationErrorFor(x => x.Title);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(10.5)]
    public void RatingOutOfRange_FailsValidation(decimal rating)
    {
        var result = _validator.TestValidate(Valid() with { Rating = rating });

        result.ShouldHaveValidationErrorFor(x => x.Rating);
    }

    [Fact]
    public void MissingPublisher_FailsValidation()
    {
        var result = _validator.TestValidate(Valid() with { Publisher = "" });

        result.ShouldHaveValidationErrorFor(x => x.Publisher);
    }
}
