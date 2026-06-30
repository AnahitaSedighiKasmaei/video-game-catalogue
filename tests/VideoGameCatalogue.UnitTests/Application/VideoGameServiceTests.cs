using FluentValidation;
using NSubstitute;
using VideoGameCatalogue.Application.Abstractions.Persistence;
using VideoGameCatalogue.Application.Common.Exceptions;
using VideoGameCatalogue.Application.VideoGames;
using VideoGameCatalogue.Application.VideoGames.Dtos;
using VideoGameCatalogue.Application.VideoGames.Validation;
using VideoGameCatalogue.Domain.Entities;

namespace VideoGameCatalogue.UnitTests.Application;

public class VideoGameServiceTests
{
    private readonly IVideoGameRepository _repository = Substitute.For<IVideoGameRepository>();
    private readonly IValidator<UpdateVideoGameRequest> _validator = new UpdateVideoGameRequestValidator();
    private readonly VideoGameService _service;

    public VideoGameServiceTests()
    {
        _service = new VideoGameService(_repository, _validator);
    }

    private static VideoGame Game() => new(
        "Hades", "Roguelike", "PC", "Supergiant Games", new DateOnly(2020, 9, 17), 9.3m);

    private static UpdateVideoGameRequest UpdateRequest() => new(
        "Hades II", "Roguelike", "PC", "Supergiant Games", new DateOnly(2024, 9, 26), 9.5m, "Sequel");

    [Fact]
    public async Task GetByIdAsync_WhenGameExists_ReturnsDto()
    {
        _repository.GetByIdAsync(1, Arg.Any<CancellationToken>()).Returns(Game());

        var dto = await _service.GetByIdAsync(1);

        Assert.Equal("Hades", dto.Title);
    }

    [Fact]
    public async Task GetByIdAsync_WhenGameMissing_ThrowsNotFound()
    {
        _repository.GetByIdAsync(99, Arg.Any<CancellationToken>()).Returns((VideoGame?)null);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.GetByIdAsync(99));
    }

    [Fact]
    public async Task GetAllAsync_PassesSearchToRepository_AndMapsResults()
    {
        _repository.GetAllAsync("hades", Arg.Any<CancellationToken>()).Returns(new[] { Game() });

        var result = await _service.GetAllAsync("hades");

        Assert.Single(result);
        await _repository.Received(1).GetAllAsync("hades", Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task UpdateAsync_WithValidRequest_MutatesEntityAndSaves()
    {
        var game = Game();
        _repository.GetByIdAsync(1, Arg.Any<CancellationToken>()).Returns(game);

        var dto = await _service.UpdateAsync(1, UpdateRequest());

        Assert.Equal("Hades II", dto.Title);
        Assert.Equal(9.5m, dto.Rating);
        Assert.Equal("Hades II", game.Title);
        await _repository.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task UpdateAsync_WithInvalidRequest_ThrowsBeforeTouchingRepository()
    {
        var invalid = UpdateRequest() with { Title = "", Rating = 50m };

        await Assert.ThrowsAsync<ValidationException>(() => _service.UpdateAsync(1, invalid));

        await _repository.DidNotReceive().GetByIdAsync(Arg.Any<int>(), Arg.Any<CancellationToken>());
        await _repository.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task UpdateAsync_WhenGameMissing_ThrowsNotFound()
    {
        _repository.GetByIdAsync(99, Arg.Any<CancellationToken>()).Returns((VideoGame?)null);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.UpdateAsync(99, UpdateRequest()));
    }
}
