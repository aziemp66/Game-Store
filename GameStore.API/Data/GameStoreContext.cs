using GameStore.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.API.Data;

public class GameStoreContext : DbContext
{
    protected internal GameStoreContext(DbContextOptions<GameStoreContext> options) : base(options)
    {
    }

    protected internal DbSet<Game> Games => Set<Game>();
}