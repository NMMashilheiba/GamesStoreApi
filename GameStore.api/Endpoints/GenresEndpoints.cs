using System;
using GameStore.api.Data;
using GameStore.api.Mappings;
using Microsoft.EntityFrameworkCore;

namespace GameStore.api.Endpoints;

public static class GenresEndpoints
{
    public static RouteGroupBuilder MapGenresEndpoints(this WebApplication app)
    {
        var genresGroup = app.MapGroup("genres");

        genresGroup.MapGet("/", async (GameStoreContext dbContext) =>
            await dbContext.Genres
                           .Select(genre => genre.ToDto())
                           .AsNoTracking()
                           .ToListAsync());

        return genresGroup;
    }
}
