using System;
using GameStore.Api.Dtos;

namespace GameStore.Api.Services;

public interface IGameService
{
Task<List<GameSummaryDto>> GetAllAsync();
    Task<GameDetailsDto?> GetByIdAsync(int id);
    Task<GameDetailsDto> CreateAsync(CreateGameDto dto);
    Task<GameDetailsDto?> UpdateAsync(int id, UpdateGameDto dto);
    Task<bool> DeleteAsync(int id);
}
