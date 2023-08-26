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

    async Task<IEnumerable<Game>> IGamesRepository.GetAllAsync()
    {
        return await dbContext.Games.AsNoTracking().ToListAsync();
    }

    async Task<Game> IGamesRepository.GetByIdAsync(int id)
    {
        try
        {
            return await dbContext.Games.FirstAsync(game => game.Id == id);
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

    async Task IGamesRepository.CreateAsync(Game game)
    {
        dbContext.Games.Add(game);

        try
        {
            await dbContext.SaveChangesAsync();
        }
        catch (DbUpdateException e)
        {
            throw new DbUpdateException("failed to create game : " + e.Message);
        }
        catch (Exception e)
        {
            throw new Exception($"internal server error : {e.Message}");
        }
    }

    async Task IGamesRepository.UpdateAsync(Game updatedGame)
    {
        dbContext.Games.Update(updatedGame);

        try
        {
            await dbContext.SaveChangesAsync();
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

    async Task IGamesRepository.DeleteAsync(int id)
    {
        try
        {
            await dbContext.Games.Where(game => game.Id == id).ExecuteDeleteAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"internal server error : {e.Message}");
        }
    }
}