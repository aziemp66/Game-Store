using GameStore.API.Entities;
using GameStore.API.Mock;

namespace GameStore.API.Repositories;

public class InMemGamesRepository
{
    private static readonly List<Game> games = MockData.GetGameList();

    public IEnumerable<Game> GetAll()
    {
        return games;
    }

    public Game GetById(int id)
    {
        try
        {
            return games.First(game => game.Id == id);
        }
        catch (InvalidOperationException e)
        {
            throw new InvalidOperationException($"can't find game by the id of {id} : {e.Message}");
        }
        catch (ArgumentNullException e)
        {
            throw new ArgumentNullException(e.Message);
        }
        catch (Exception e)
        {
            throw new Exception($"internal server error : {e.Message}");
        }
    }

    public Game Create(Game game)
    {
        if (!games.Any())
        {
            game.Id = 1;
            games.Add(game);
            return game;
        }

        game.Id = games.Max(game => game.Id) + 1;
        games.Add(game);
        return game;
    }

    public void Update(Game updatedGame)
    {
        int index;
        try
        {
            index = games.FindIndex(game => game.Id == updatedGame.Id);
        }
        catch (ArgumentNullException e)
        {
            throw new ArgumentNullException(e.Message);
        }
        catch (Exception e)
        {
            throw new Exception($"internal server error : {e.Message}");
        }
        games[index] = updatedGame;
    }

    public void Delete(int id)
    {
        int index;
        try
        {
            index = games.FindIndex(game => game.Id == id);
        }
        catch (ArgumentNullException e)
        {
            throw new ArgumentNullException($"can't find game by the id of {id} : {e.Message}");
        }
        catch (Exception e)
        {
            throw new Exception($"internal server error : {e.Message}");
        }
        games.RemoveAt(index);
    }
}
