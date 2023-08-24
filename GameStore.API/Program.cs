using System.Text.Json;
using GameStore.API.Entities;
using GameStore.API.Mock;

const string GetGameByIdRouteName = "GetGameById";

List<Game> games = Mock.GetGameList();

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/games", async (ctx) =>
{
	ctx.Response.ContentType = "application/json";
	await JsonSerializer.SerializeAsync(ctx.Response.Body, games);
});

app.MapGet("/games/{id}", async (ctx) =>
{
	ctx.Response.ContentType = "application/json";
	int id = Convert.ToInt32(ctx.Request.RouteValues["id"]);
	try
	{
		Game game = games.First(g => g.Id == id);
		await JsonSerializer.SerializeAsync(ctx.Response.Body, game);
	}
	catch (System.Exception)
	{
		ctx.Response.StatusCode = 404;
		await ctx.Response.WriteAsJsonAsync(new { message = "Game not found." });
	}
}).WithName(GetGameByIdRouteName);

app.MapPost("/games", (Game game) =>
{
	game.Id = games.Max(g => g.Id) + 1;
	games.Add(game);

	return Results.CreatedAtRoute(GetGameByIdRouteName, new { id = game.Id }, game);
});

app.MapPut("/games/{id}", async (ctx) =>
{
	ctx.Response.ContentType = "application/json";
	int id = Convert.ToInt32(ctx.Request.RouteValues["id"]);

	Game game;
	try
	{
		game = games.First(g => g.Id == id);
	}
	catch (System.Exception)
	{
		ctx.Response.StatusCode = 404;
		await ctx.Response.WriteAsJsonAsync(new { message = "Game not found." });
		return;
	}

	Game? UpdateGame = await ctx.Request.ReadFromJsonAsync<Game>();
	if (UpdateGame is null)
	{
		ctx.Response.StatusCode = 400;
		await ctx.Response.WriteAsJsonAsync(new { message = "Invalid game data." });
		return;
	}

	game = new Game
	{
		Id = id,
		Name = UpdateGame.Name,
		Genre = UpdateGame.Genre,
		Price = UpdateGame.Price,
		ReleaseDate = UpdateGame.ReleaseDate,
		ImageUri = UpdateGame.ImageUri
	};

	ctx.Response.Headers.Add("Location", $"{ctx.Request.Scheme}://{ctx.Request.Host}/games/{game.Id}");
	await ctx.Response.WriteAsJsonAsync(new { message = "Game updated." });
});

app.Run();
