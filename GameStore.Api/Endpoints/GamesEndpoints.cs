using System;
using GameStore.Api.Dtos;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";

private static readonly List<GameDto> games =
[ new GameDto(1, "The Legend of Zelda: Breath of the Wild", "Action-adventure", 59.99m, new DateOnly(2017, 3, 3)),
    new GameDto(2, "Super Mario Odyssey", "Platformer", 59.99m, new DateOnly(2017, 10, 27)),
    new GameDto(3, "Red Dead Redemption 2", "Action-adventure", 59.99m, new DateOnly(2018, 10, 26)),
    new GameDto(4, "The Witcher 3: Wild Hunt", "Action RPG", 39.99m, new DateOnly(2015, 5, 19)),
    new GameDto(5, "Minecraft", "Sandbox", 26.95m, new DateOnly(2011, 11, 18))
    ];

    public static void MapGamesEndpoint(this WebApplication app)
    {

var group = app.MapGroup("/games");

        app.MapGet("/", () => games);


//GET /games/{id}
group.MapGet("/{id}", (int id)=>{
   var game= games.Find(game=> game.Id == id);
   return game is null ?Results.NotFound():Results.Ok(game);
    }
).WithName(GetGameEndpointName);
//POST /games
group.MapPost("/",(CreateGameDto newGame) =>
{
    // if (string.IsNullOrEmpty(newGame.Name))
    // {
    //     return Results.BadRequest("الاسم مطلوب");
    // }
    GameDto game =new(
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );
    games.Add(game);
    return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
}
);

//PUT/games/{id}
group.MapPut("/{id}",(int id, UpdateGameDto updateGame)=>
{

    var index =games.FindIndex(game => game.Id == id);
        if(index == -1)
    {
        return Results.NotFound();
    }
    games[index]= new GameDto(
        id,
        updateGame.Name,
        updateGame.Genre,
        updateGame.Price,
        updateGame.ReleaseDate

    );
    return  Results.Ok(games[index]);
}
);
group.MapDelete("/{id}",(int id)=>
{
    games.RemoveAll(game => game.Id == id);
    return Results.NoContent();
}
);


    }
   
}
