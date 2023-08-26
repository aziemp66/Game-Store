
using GameStore.API.Entities;

namespace GameStore.API.Repositories
{
    public interface IGamesRepository
    {
        Task<IEnumerable<Game>> GetAllAsync();
        Task<Game> GetByIdAsync(int id);
        Task CreateAsync(Game game);
        Task UpdateAsync(Game updatedGame);
        Task DeleteAsync(int id);
    }
}