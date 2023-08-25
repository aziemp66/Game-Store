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
            (IGamesRepository gamesRepository) =>
            {
                try
                {
                    var games = gamesRepository.GetAll();
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
                (IGamesRepository gamesRepository, int id) =>
                {
                    try
                    {
                        var game = gamesRepository.GetById(id);
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
            (IGamesRepository gamesRepository, Game game) =>
            {
                try
                {
                    var result = gamesRepository.Create(game);
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
            "/{id:int}", (IGamesRepository gamesRepository, int id, Game updatedGame) =>
            {
                try
                {
                    gamesRepository.GetById(id);
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
                    gamesRepository.Update(updatedGame);
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
            (IGamesRepository gamesRepository, int id) =>
            {
                Game game;
                try
                {
                    game = gamesRepository.GetById(id);
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
                    gamesRepository.Delete(id);
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