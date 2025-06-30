using System;
using GameStore.api.Dtos;

namespace GameStore.api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";

    static List<GameDto> games = [
        new(
            1,
            "Street Fighter II",
            "Fighting",
            19.99M,
            new DateOnly(1992, 7, 15)
        ),
        new(
            2,
            "Final Fantasy XIV",
            "Roleplaying",
            55.99M,
            new DateOnly(2010, 9, 30)
        ),
        new(
            3,
            "FIFA 2023",
            "Sports",
            69.99M,
            new DateOnly(2022, 9, 27)
        )

    ];

    public static RouteGroupBuilder MapGamesEndpints(this WebApplication app)
    {
        var gamesGroup = app.MapGroup("games")
                            .WithParameterValidation();

        // GET /games
        gamesGroup.MapGet("/", () => games);

        // GET /games/{id}
        gamesGroup.MapGet("/{id}", (int id) =>
        {
            GameDto? game = games.Find(game => game.Id == id);

            return game is null ? Results.NotFound() : Results.Ok(game);
        }).WithName(GetGameEndpointName);



        // POST /games
        gamesGroup.MapPost("/", (CreateGameDto newGame) =>
        {
            GameDto game = new(
                games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );
            games.Add(game);

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
        });


        // PUT /games/{id}
        gamesGroup.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
        {
            var index = games.FindIndex(game => game.Id == id);

            // Another way is to CREATE a new entry with the payload
            if (index == -1)
            {
                return Results.NotFound();
            }
            games[index] = new GameDto(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate

            );

            return Results.NoContent();
        });

        // DELETE /games/{id}
        gamesGroup.MapDelete("/{id}", (int id) =>
        {
            // games.RemoveAll(game => game.Id == id);

            return games.RemoveAll(game => game.Id == id) == 1 ? Results.NoContent() : Results.NotFound();

        });
        return gamesGroup;
    }
}
