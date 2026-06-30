namespace VideoGameCatalogue.Application.VideoGames.Dtos;

/// <summary>
/// Incoming payload for editing a game. The identifier comes from the route, not the body,
/// so it is intentionally absent here.
/// </summary>
public sealed record UpdateVideoGameRequest(
    string Title,
    string Genre,
    string Platform,
    string Publisher,
    DateOnly ReleaseDate,
    decimal Rating,
    string? Description);
