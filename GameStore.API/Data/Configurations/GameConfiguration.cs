using GameStore.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.API.Data.Configurations;

public class GameConfiguration : IEntityTypeConfiguration<Game>
{
    void IEntityTypeConfiguration<Game>.Configure(EntityTypeBuilder<Game> builder)
    {
        builder.Property(game => game.Price).HasPrecision(6, 2);
    }
}