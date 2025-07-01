using System.ComponentModel.DataAnnotations;

namespace GameStore.api.Dtos;

public record class CreateGameDto(
    [Required][StringLength(60)] string Name,
    int GenreId,
    [Range(1, 110)]decimal Price,
    DateOnly ReleaseDate
);
