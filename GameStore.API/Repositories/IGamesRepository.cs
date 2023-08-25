
using GameStore.API.Entities;

namespace GameStore.API.Repositories
{
    public interface IGamesRepository
    {
        IEnumerable<Game> GetAll();
        Game GetById(int id);
        Game Create(Game game);
        void Update(Game updatedGame);
        void Delete(int id);

    }
}