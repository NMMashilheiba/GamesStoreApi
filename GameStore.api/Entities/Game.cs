using System;

namespace GameStore.api.Entities;

public class Game
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public decimal Price { get; set; }
    public DateOnly ReleaseDate { get; set; }
    // foreign key
    public int GenreId { get; set; }

    // nagivation properties
    public Genre? Genre { get; set; }
}
