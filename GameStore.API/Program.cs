using GameStore.API.Entities;
using GameStore.API.Mock;

List<Game> games = Mock.GetGameList();

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
