using VideoGameCatalogue.Domain.Entities;
using VideoGameCatalogue.Domain.Exceptions;

namespace VideoGameCatalogue.UnitTests.Domain;

public class VideoGameTests
{
    private static VideoGame CreateValid() => new(
        title: "Celeste",
        genre: "Platformer",
        platform: "PC",
        publisher: "Maddy Makes Games",
        releaseDate: new DateOnly(2018, 1, 25),
        rating: 9.1m);

    [Fact]
    public void Constructor_WithValidData_SetsProperties()
    {
        var game = CreateValid();

        Assert.Equal("Celeste", game.Title);
        Assert.Equal(9.1m, game.Rating);
        Assert.Null(game.Description);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Constructor_WithMissingTitle_Throws(string? title)
    {
        var ex = Assert.Throws<DomainValidationException>(() => new VideoGame(
            title!, "Genre", "Platform", "Publisher", new DateOnly(2020, 1, 1), 8m));

        Assert.Contains("Title", ex.Message);
    }

    [Fact]
    public void Constructor_WithTitleExceedingMaxLength_Throws()
    {
        var longTitle = new string('a', VideoGame.TitleMaxLength + 1);

        Assert.Throws<DomainValidationException>(() => new VideoGame(
            longTitle, "Genre", "Platform", "Publisher", new DateOnly(2020, 1, 1), 8m));
    }

    [Theory]
    [InlineData(-0.1)]
    [InlineData(10.1)]
    public void Constructor_WithRatingOutOfRange_Throws(decimal rating)
    {
        Assert.Throws<DomainValidationException>(() => new VideoGame(
            "Title", "Genre", "Platform", "Publisher", new DateOnly(2020, 1, 1), rating));
    }

    [Fact]
    public void Constructor_TrimsTextFields_AndNormalisesBlankDescriptionToNull()
    {
        var game = new VideoGame("  Title  ", " Genre ", " PC ", " Pub ", new DateOnly(2020, 1, 1), 8m, "   ");

        Assert.Equal("Title", game.Title);
        Assert.Equal("Genre", game.Genre);
        Assert.Null(game.Description);
    }

    [Fact]
    public void Update_AppliesNewValues()
    {
        var game = CreateValid();

        game.Update("Celeste 64", "Platformer", "Switch", "Maddy Makes Games", new DateOnly(2024, 3, 30), 8.5m, "Tribute");

        Assert.Equal("Celeste 64", game.Title);
        Assert.Equal("Switch", game.Platform);
        Assert.Equal(8.5m, game.Rating);
        Assert.Equal("Tribute", game.Description);
    }

    [Fact]
    public void Update_WithInvalidRating_Throws()
    {
        var game = CreateValid();

        Assert.Throws<DomainValidationException>(() => game.Update(
            "Celeste", "Platformer", "PC", "Maddy Makes Games", new DateOnly(2018, 1, 25), 11m));
    }
}
