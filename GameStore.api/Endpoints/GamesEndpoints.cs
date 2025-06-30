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

    public static WebApplication MapGamesEndpints(this WebApplication app)
    {

        // GET /games
        app.MapGet("games", () => games);

        // GET /games/{id}
        app.MapGet("games/{id}", (int id) =>
        {
            GameDto? game = games.Find(game => game.Id == id);

            return game is null ? Results.NotFound() : Results.Ok(game);
        }).WithName(GetGameEndpointName);



        // POST /games
        app.MapPost("games", (CreateGameDto newGame) =>
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
        app.MapPut("games/{id}", (int id, UpdateGameDto updatedGame) =>
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
        app.MapDelete("games/{id}", (int id) =>
        {
            // games.RemoveAll(game => game.Id == id);

            return games.RemoveAll(game => game.Id == id) == 1 ? Results.NoContent() : Results.NotFound();
            // Console.WriteLine(deleteGame);

            // return Results.NoContent();
        });
        return app;
    }
}
