using GameStore.API.Dtos;

namespace GameStore.API.Entities;

public static class EntityExtensions
{
    public static GameDto AsDto(this Game game) =>
        new(
            game.Id,
            game.Name,
            game.Genre,
            game.Price,
            game.ReleaseDate,
            game.ImageUri
        );
}