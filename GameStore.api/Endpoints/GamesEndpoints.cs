using System;
using GameStore.api.Data;
using GameStore.api.Dtos;
using GameStore.api.Entities;
using GameStore.api.Mappings;
using Microsoft.EntityFrameworkCore;

namespace GameStore.api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";

    public static RouteGroupBuilder MapGamesEndpints(this WebApplication app)
    {
        var gamesGroup = app.MapGroup("games")
                            .WithParameterValidation();

        // GET /games
        gamesGroup.MapGet("/", (GameStoreContext dbContext) =>
            dbContext.Games
                     .Include(game => game.Genre)
                     .Select(game => game.ToGameSummaryDto())
                     .AsNoTracking());

        // GET /games/{id}
        gamesGroup.MapGet("/{id}", (int id, GameStoreContext dbContext) =>
        {
            Game? game = dbContext.Games.Find(id);

        return game is null ? Results.NotFound() : Results.Ok(game.ToGameDetailsDto());
        }).WithName(GetGameEndpointName);

        // POST /games
        gamesGroup.MapPost("/", (CreateGameDto newGame, GameStoreContext dbContext) =>
        {
            Game game = newGame.ToEntity();
            // game.Genre = dbContext.Genres.Find(newGame.GenreId);

            dbContext.Games.Add(game);
            dbContext.SaveChanges();

        return Results.CreatedAtRoute(
            GetGameEndpointName,
            new { id = game.Id },
            game.ToGameDetailsDto());
        });


        // PUT /games/{id}
        gamesGroup.MapPut("/{id}", (int id, UpdateGameDto updatedGame, GameStoreContext dbContext) =>
        {
            var existingGame = dbContext.Games.Find(id);

            // Another way is to CREATE a new entry with the payload
            if (existingGame is null)
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingGame)
                     .CurrentValues
                     .SetValues(updatedGame.ToEntity(id));
            dbContext.SaveChanges();

            return Results.NoContent();
        });

        // DELETE /games/{id}
        gamesGroup.MapDelete("/{id}", (int id, GameStoreContext dbContext) =>
        {
            dbContext.Games
                     .Where(game => game.Id == id)
                     .ExecuteDelete();

            return Results.NoContent();

        });
        return gamesGroup;
    }
}
