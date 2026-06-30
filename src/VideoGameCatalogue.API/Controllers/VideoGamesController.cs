using Microsoft.AspNetCore.Mvc;
using VideoGameCatalogue.Application.VideoGames;
using VideoGameCatalogue.Application.VideoGames.Dtos;

namespace VideoGameCatalogue.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class VideoGamesController : ControllerBase
{
    private readonly IVideoGameService _service;

    public VideoGamesController(IVideoGameService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<VideoGameDto>>> GetAll([FromQuery] string? search, CancellationToken cancellationToken)
    {
        var games = await _service.GetAllAsync(search, cancellationToken);
        return Ok(games);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<VideoGameDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var game = await _service.GetByIdAsync(id, cancellationToken);
        return Ok(game);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<VideoGameDto>> Update(int id, UpdateVideoGameRequest request, CancellationToken cancellationToken)
    {
        var updated = await _service.UpdateAsync(id, request, cancellationToken);
        return Ok(updated);
    }
}
