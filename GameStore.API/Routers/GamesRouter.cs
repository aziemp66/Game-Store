using GameStore.API.Dtos;
using GameStore.API.Entities;
using GameStore.API.Repositories;

namespace GameStore.API.Routers;

public static class GamesRouter
{
    const string GetGameByIdRouteName = "GetGameById";

    public static RouteGroupBuilder MapGamesRouter(this IEndpointRouteBuilder routes)
    {
        var gameRoutes = routes.MapGroup("/games").WithParameterValidation();

        gameRoutes.MapGet(
            "/",
            async (IGamesRepository gamesRepository) =>
            {
                try
                {
                    var games = await gamesRepository.GetAllAsync();
                    return Results.Ok(games.Select(game => game.AsDto()));
                }
                catch (System.Exception)
                {
                    return Results.Problem("internal server error");
                }
            }
        );

        gameRoutes
            .MapGet(
                "/{id:int}",
                async (IGamesRepository gamesRepository, int id) =>
                {
                    try
                    {
                        var game = await gamesRepository.GetByIdAsync(id);
                        return Results.Ok(game.AsDto());
                    }
                    catch (InvalidOperationException e)
                    {
                        return Results.NotFound(e.Message);
                    }
                    catch (ArgumentNullException e)
                    {
                        return Results.NotFound(e.Message);
                    }
                    catch (Exception)
                    {
                        return Results.Problem("internal server error");
                    }
                }
            )
            .WithName(GetGameByIdRouteName);

        gameRoutes.MapPost(
            "/",
            async (IGamesRepository gamesRepository, CreateGameDto gameDto) =>
            {
                var game = new Game()
                {
                    Name = gameDto.Name,
                    Genre = gameDto.Genre,
                    Price = gameDto.Price,
                    ReleaseDate = gameDto.ReleaseDate,
                    ImageUri = gameDto.ImageUri
                };

                try
                {
                    await gamesRepository.CreateAsync(game);
                    return Results.CreatedAtRoute(GetGameByIdRouteName, new { id = game.Id }, game);
                }
                catch (InvalidOperationException e)
                {
                    return Results.Problem(e.Message);
                }
                catch (ArgumentNullException e)
                {
                    return Results.Problem(e.Message);
                }
                catch (Exception)
                {
                    return Results.Problem("internal server error");
                }
            }
        );
        gameRoutes.MapPut(
            "/{id:int}", async (IGamesRepository gamesRepository, int id, UpdateGameDto updatedGameDto) =>
            {
                Game existingGame;
                try
                {
                    existingGame = await gamesRepository.GetByIdAsync(id);
                }
                catch (InvalidOperationException e)
                {
                    return Results.NotFound(e.Message);
                }
                catch (ArgumentNullException e)
                {
                    return Results.NotFound(e.Message);
                }
                catch (Exception e)
                {
                    return Results.Problem($"internal server error : {e.Message}");
                }

                existingGame.Name = updatedGameDto.Name;
                existingGame.Genre = updatedGameDto.Genre;
                existingGame.Price = updatedGameDto.Price;
                existingGame.ReleaseDate = updatedGameDto.ReleaseDate;
                existingGame.ImageUri = updatedGameDto.ImageUri;

                try
                {
                    await gamesRepository.UpdateAsync(existingGame);
                    return Results.Ok(existingGame);
                }
                catch (ArgumentNullException)
                {
                    return Results.NotFound();
                }
                catch (Exception e)
                {
                    return Results.Problem($"internal server error {e.Message}");
                }
            }
        );

        gameRoutes.MapDelete(
            "/{id:int}",
            async (IGamesRepository gamesRepository, int id) =>
            {
                Game game;
                try
                {
                    game = await gamesRepository.GetByIdAsync(id);
                }
                catch (InvalidOperationException e)
                {
                    return Results.NotFound(e.Message);
                }
                catch (ArgumentNullException e)
                {
                    return Results.NotFound(e.Message);
                }
                catch (Exception)
                {
                    return Results.Problem("internal server error");
                }

                try
                {
                    await gamesRepository.DeleteAsync(id);
                    return Results.Ok();
                }
                catch (ArgumentNullException e)
                {
                    return Results.NotFound(e.Message);
                }
                catch (Exception)
                {
                    return Results.Problem("internal server error");
                }
            }
        );
        return gameRoutes;
    }
}