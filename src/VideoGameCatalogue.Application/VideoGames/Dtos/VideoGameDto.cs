namespace VideoGameCatalogue.Application.VideoGames.Dtos;

/// <summary>
/// Read model returned to clients. Decoupled from the entity so the persistence
/// shape can evolve without breaking the API contract.
/// </summary>
public sealed record VideoGameDto(
    int Id,
    string Title,
    string Genre,
    string Platform,
    string Publisher,
    DateOnly ReleaseDate,
    decimal Rating,
    string? Description);
