using Microsoft.AspNetCore.Mvc;
using GameStore.Api.Dtos;
using GameStore.Api.Services;

namespace GameStore.Api.Controllers;

[ApiController]
[Route("api/games")]
public class GamesController(IGameService service) : ControllerBase
{
    private readonly IGameService _service = service;

   

    // GET: api/games
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    // GET: api/games/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    // POST: api/games
    [HttpPost]
    public async Task<IActionResult> Create(CreateGameDto dto)
    {
        var result = await _service.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Id },
            result
        );
    }

    // PUT: api/games/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateGameDto dto)
    {
        var result = await _service.UpdateAsync(id, dto);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    // DELETE: api/games/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}