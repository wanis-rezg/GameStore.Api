using GameStore.Api.Data;
using GameStore.Api.Endpoints;
using GameStore.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();
builder.AddGameStoreDb();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();

app.MapGenresEndpoints();
app.MigrateDb();

app.Run();
