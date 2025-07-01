using System;
using GameStore.api.Dtos;
using GameStore.api.Entities;

namespace GameStore.api.Mappings;

public static class GameMapping
{
    public static Game ToEntity(this CreateGameDto game)
    {
        return new Game()
        {
            Name = game.Name,
            GenreId = game.GenreId,
            Price = game.Price,
            ReleaseDate = game.ReleaseDate
        };
    }

    public static GameDto ToDto(this Game game)
    {
        return new (
                game.Id,
                game.Name,
                game.Genre!.name,
                game.Price,
                game.ReleaseDate

            );
    }
}
