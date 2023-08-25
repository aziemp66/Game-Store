using GameStore.API.Entities;

namespace GameStore.API.Repositories;

public class InMemGamesRepository : IGamesRepository
{
    private readonly List<Game> games = new()
        {
            new Game
            {
                Id = 1,
                Name = "The Witcher 3: Wild Hunt",
                Genre = "Action RPG",
                Price = 49.99m,
                ReleaseDate = DateTime.Parse("2015-05-19"),
                ImageUri = "https://placehold.co/100"
            },
            new Game
            {
                Id = 2,
                Name = "Red Dead Redemption 2",
                Genre = "Action Adventure",
                Price = 59.99m,
                ReleaseDate = DateTime.Parse("2018-10-26"),
                ImageUri = "https://placehold.co/100"
            },
            new Game
            {
                Id = 3,
                Name = "The Last of Us Part II",
                Genre = "Action Adventure",
                Price = 39.99m,
                ReleaseDate = DateTime.Parse("2020-06-19"),
                ImageUri = "https://placehold.co/100"
            },
            new Game
            {
                Id = 4,
                Name = "Cyberpunk 2077",
                Genre = "Action RPG",
                Price = 59.99m,
                ReleaseDate = DateTime.Parse("2020-12-10"),
                ImageUri = "https://placehold.co/100"
            },
            new Game
            {
                Id = 5,
                Name = "Assassin's Creed Valhalla",
                Genre = "Action RPG",
                Price = 49.99m,
                ReleaseDate = DateTime.Parse("2020-11-10"),
                ImageUri = "https://placehold.co/100"
            }
        };

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
