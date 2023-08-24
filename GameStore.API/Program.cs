using System.Text.Json;
using GameStore.API.Entities;
using GameStore.API.Mock;

const string GetGameByIdRouteName = "GetGameById";

List<Game> games = Mock.GetGameList();

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var gameRoutes = app.MapGroup("/games");

app.MapGet("/", () => "Hello World!");

gameRoutes.MapGet("/", async (ctx) =>
{
	ctx.Response.ContentType = "application/json";
	await JsonSerializer.SerializeAsync(ctx.Response.Body, games);
});

gameRoutes.MapGet("/{id}", async (ctx) =>
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

gameRoutes.MapPost("/", (Game game) =>
{
	game.Id = games.Max(g => g.Id) + 1;
	games.Add(game);

	return Results.CreatedAtRoute(GetGameByIdRouteName, new { id = game.Id }, game);
});

gameRoutes.MapPut("/{id}", async (ctx) =>
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

gameRoutes.MapDelete("/games/{id}", async (ctx) =>
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

	games.Remove(game);

	await ctx.Response.WriteAsJsonAsync(new { message = "Game deleted." });
});

app.Run();
