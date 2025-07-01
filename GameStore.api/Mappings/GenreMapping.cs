using System;
using GameStore.api.Dtos;
using GameStore.api.Entities;

namespace GameStore.api.Mappings;

public static class GenreMapping
{
    public static GenreDto ToDto(this Genre genre)
    {
        return new GenreDto(
            genre.Id,
            genre.name);
    }
}
