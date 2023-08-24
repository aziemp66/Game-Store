using System.Text.Json;
using GameStore.API.Entities;
using GameStore.API.Mock;

List<Game> games = Mock.GetGameList();

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/games", async (ctx) =>
{
	ctx.Response.ContentType = "application/json";
	await JsonSerializer.SerializeAsync(ctx.Response.Body, games);
});

app.Run();
