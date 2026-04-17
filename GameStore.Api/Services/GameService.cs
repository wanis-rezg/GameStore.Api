using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Models;
using GameStore.Api.Services;
using Microsoft.EntityFrameworkCore;

public class GameService(GameStoreContext _db) : IGameService
{
 

    public async Task<List<GameSummaryDto>> GetAllAsync()
    {
        return await _db.Games
            .Include(game => game.Genre)
            .Select(game => new GameSummaryDto(
                 game.Id,
    game.Name,
    game.Genre!.Name,
    game.Price,
    game.ReleaseDate
            ))
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<GameDetailsDto?> GetByIdAsync(int id)
    {
        var game = await _db.Games.FindAsync(id);

        if (game == null) return null;

        return new GameDetailsDto(
            game.Id,
            game.Name,
            game.GenreId,
            game.Price,
            game.ReleaseDate
        );
    }

    public async Task<GameDetailsDto> CreateAsync(CreateGameDto dto)
    {
        var game = new Game
        {
            Name = dto.Name,
            GenreId = dto.GenreId,
            Price = dto.Price,
            ReleaseDate = dto.ReleaseDate
        };

        _db.Games.Add(game);
        await _db.SaveChangesAsync();

        return new GameDetailsDto(
            game.Id,
            game.Name,
            game.GenreId,
            game.Price,
            game.ReleaseDate
        );
    }

    public async Task<GameDetailsDto?> UpdateAsync(int id, UpdateGameDto dto)
    {
        var game = await _db.Games.FindAsync(id);
        if (game == null) return null;

        game.Name = dto.Name;
        game.GenreId = dto.GenreId;
        game.Price = dto.Price;
        game.ReleaseDate = dto.ReleaseDate;

        await _db.SaveChangesAsync();

        return new GameDetailsDto(
            game.Id,
            game.Name,
            game.GenreId,
            game.Price,
            game.ReleaseDate
        );
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var affected = await _db.Games
            .Where(g => g.Id == id)
            .ExecuteDeleteAsync();

        return affected > 0;
    }
}