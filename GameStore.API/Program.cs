
using GameStore.API.Repositories;
using GameStore.API.Routers;

var builder = WebApplication.CreateBuilder(args);

// Not The Best Option for real world applications
// only for demo purposes
// for real database use AddScoped
builder.Services.AddSingleton<IGamesRepository, InMemGamesRepository>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGamesRouter();

app.Run();
