using GameStore.API.Entities;
using System.Text.Json;
using GameStore.API.Repositories;

namespace GameStore.API.Routers;

public static class GamesRouter
{
    const string GetGameByIdRouteName = "GetGameById";

    public static RouteGroupBuilder MapGamesRouter(this IEndpointRouteBuilder routes)
    {
        InMemGamesRepository gameStoreRepositories = new();
        var gameRoutes = routes.MapGroup("/games").WithParameterValidation();

        gameRoutes.MapGet(
            "/",
            () =>
            {
                try
                {
                    var games = gameStoreRepositories.GetAll();
                    return Results.Ok(games);
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
                (int id) =>
                {
                    try
                    {
                        var game = gameStoreRepositories.GetById(id);
                        return Results.Ok(game);
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
            (Game game) =>
            {
                try
                {
                    var result = gameStoreRepositories.Create(game);
                    return Results.CreatedAtRoute(GetGameByIdRouteName, new { id = result.Id }, result);
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
            "/{id:int}", (int id, Game updatedGame) =>
            {
                try
                {
                    gameStoreRepositories.GetById(id);
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

                try
                {
                    updatedGame.Id = id;
                    gameStoreRepositories.Update(updatedGame);
                    return Results.Ok(updatedGame);
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
            (int id) =>
            {
                Game game;
                try
                {
                    game = gameStoreRepositories.GetById(id);
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
                    gameStoreRepositories.Delete(id);
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