
using GameStore.API.Routers;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGamesRouter();

app.Run();
