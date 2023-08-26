
using GameStore.API.Data;
using GameStore.API.Repositories;
using GameStore.API.Routers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Not The Best Option for real world applications
// only for demo purposes
// for real database use AddScoped
builder.Services.AddSingleton<IGamesRepository, InMemGamesRepository>();

var connString = builder.Configuration.GetConnectionString("GameStoreContext");
builder.Services.AddSqlServer<GameStoreContext>(connString);

var app = builder.Build();

app.Services.InitializeDb();

app.MapGet("/", () => "Hello World!");

app.MapGamesRouter();

app.Run();
