using FluentValidation;
using VideoGameCatalogue.Application.VideoGames.Dtos;
using VideoGameCatalogue.Domain.Entities;

namespace VideoGameCatalogue.Application.VideoGames.Validation;

/// <summary>
/// Validates the shape of an edit request before any business logic runs.
/// Domain invariants are enforced again by the entity; this layer gives callers
/// fast, field-level feedback.
/// </summary>
public sealed class UpdateVideoGameRequestValidator : AbstractValidator<UpdateVideoGameRequest>
{
    public UpdateVideoGameRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(VideoGame.TitleMaxLength);

        RuleFor(x => x.Genre).NotEmpty();
        RuleFor(x => x.Platform).NotEmpty();
        RuleFor(x => x.Publisher).NotEmpty();

        RuleFor(x => x.Rating)
            .InclusiveBetween(VideoGame.MinRating, VideoGame.MaxRating);

        RuleFor(x => x.ReleaseDate)
            .NotEmpty();
    }
}
