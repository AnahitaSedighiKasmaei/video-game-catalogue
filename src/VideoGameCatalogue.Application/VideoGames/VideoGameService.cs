using FluentValidation;
using VideoGameCatalogue.Application.Abstractions.Persistence;
using VideoGameCatalogue.Application.Common.Exceptions;
using VideoGameCatalogue.Application.VideoGames.Dtos;
using VideoGameCatalogue.Application.VideoGames.Mapping;
using VideoGameCatalogue.Domain.Entities;

namespace VideoGameCatalogue.Application.VideoGames;

public sealed class VideoGameService : IVideoGameService
{
    private readonly IVideoGameRepository _repository;
    private readonly IValidator<UpdateVideoGameRequest> _updateValidator;

    public VideoGameService(
        IVideoGameRepository repository,
        IValidator<UpdateVideoGameRequest> updateValidator)
    {
        _repository = repository;
        _updateValidator = updateValidator;
    }

    public async Task<IReadOnlyList<VideoGameDto>> GetAllAsync(string? search, CancellationToken cancellationToken = default)
    {
        var games = await _repository.GetAllAsync(search, cancellationToken);
        return games.Select(g => g.ToDto()).ToList();
    }

    public async Task<VideoGameDto> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var game = await _repository.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException(nameof(VideoGame), id);

        return game.ToDto();
    }

    public async Task<VideoGameDto> UpdateAsync(int id, UpdateVideoGameRequest request, CancellationToken cancellationToken = default)
    {
        await _updateValidator.ValidateAndThrowAsync(request, cancellationToken);

        var game = await _repository.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException(nameof(VideoGame), id);

        game.Update(
            request.Title,
            request.Genre,
            request.Platform,
            request.Publisher,
            request.ReleaseDate,
            request.Rating,
            request.Description);

        await _repository.SaveChangesAsync(cancellationToken);

        return game.ToDto();
    }
}
