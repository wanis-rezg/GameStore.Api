using System;
using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";

private static readonly List<GameSummaryDto> games =[];

    public static void MapGamesEndpoint(this WebApplication app)
    {

var group = app.MapGroup("/games");

        app.MapGet("/", async(GameStoreContext dbContext) 
        
        => await dbContext.Games.Include(game => game.Genre)
         .
        Select(game => new GameSummaryDto(
            game.Id,
            game.Name,
            game.Genre!.Name,
            game.Price,
            game.ReleaseDate
        ) ).AsNoTracking().ToListAsync() );


//GET /games/{id}
group.MapGet("/{id}", async(int id, GameStoreContext dbContext)=>{
   var game= await dbContext.Games.FindAsync(id);
   return game is null ?Results.NotFound():Results.Ok(
    new GameDetailsDto(
        game.Id,
        game.Name,
        game.GenreId,
        game.Price,
        game.ReleaseDate)
   );
    }
).WithName(GetGameEndpointName);


//POST /games
group.MapPost("/",async(CreateGameDto newGame, GameStoreContext dbContext) =>
{
    // if (string.IsNullOrEmpty(newGame.Name))
    // {
    //     return Results.BadRequest("الاسم مطلوب");
    // }
 Game game = new()
 {
   Name = newGame.Name,
   GenreId = newGame.GenreId,
   Price = newGame.Price,
    ReleaseDate = newGame.ReleaseDate
 };
  dbContext.Games.Add(game);
  await dbContext.SaveChangesAsync();
  GameDetailsDto gameDto = new (
game.Id,
game.Name,
game.GenreId,
game.Price,
game.ReleaseDate
  );
    return Results.CreatedAtRoute(GetGameEndpointName, new { id = gameDto.Id }, gameDto);
}
);

//PUT/games/{id}
group.MapPut("/{id}",async(int id, 
UpdateGameDto updateGame,
 GameStoreContext dbContext)=>
{
        var existingGame = await dbContext.Games.FindAsync(id);

         if(existingGame is null)
    {
        return Results.NotFound();
    }
    existingGame.Name = updateGame.Name;
    existingGame.GenreId = updateGame.GenreId;
    existingGame.Price = updateGame.Price;
    existingGame.ReleaseDate = updateGame.ReleaseDate;

   await dbContext.SaveChangesAsync();
    return  Results.Ok(
    new GameDetailsDto(
        existingGame.Id,
        existingGame.Name,
        existingGame.GenreId,
        existingGame.Price,
        existingGame.ReleaseDate
        )
    );
}
);
group.MapDelete("/{id}",async(int id , GameStoreContext dbContext)=>
{
    var game = await dbContext.Games.Where
    (game => game.Id == id).ExecuteDeleteAsync();
    
     return Results.NoContent();
}
);


    }
   
}
