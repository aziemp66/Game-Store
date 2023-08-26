using GameStore.API.Data;
using GameStore.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.API.Repositories;

public class EntityFrameworkGamesRepository : IGamesRepository
{
    private readonly GameStoreContext dbContext;

    public EntityFrameworkGamesRepository(GameStoreContext dbContext)
    {
        this.dbContext = dbContext;
    }

    IEnumerable<Game> IGamesRepository.GetAll()
    {
        return dbContext.Games.AsNoTracking().ToList();
    }

    Game IGamesRepository.GetById(int id)
    {
        try
        {
            return dbContext.Games.First(game => game.Id == id);
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

    Game IGamesRepository.Create(Game game)
    {
        dbContext.Games.Add(game);

        try
        {
            dbContext.SaveChanges();
        }
        catch (DbUpdateException e)
        {
            throw new DbUpdateException("failed to create game : " + e.Message);
        }
        catch (Exception e)
        {
            throw new Exception($"internal server error : {e.Message}");
        }

        return game;
    }

    void IGamesRepository.Update(Game updatedGame)
    {
        dbContext.Games.Update(updatedGame);

        try
        {
            dbContext.SaveChanges();
        }
        catch (DbUpdateException e)
        {
            throw new DbUpdateException("failed to update game : " + e.Message);
        }
        catch (Exception e)
        {
            throw new Exception($"internal server error : {e.Message}");
        }
    }

    void IGamesRepository.Delete(int id)
    {
        try
        {
            dbContext.Games.Where(game => game.Id == id).ExecuteDelete();
        }
        catch (Exception e)
        {
            throw new Exception($"internal server error : {e.Message}");
        }
    }
}