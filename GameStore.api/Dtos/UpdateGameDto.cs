using System.ComponentModel.DataAnnotations;

namespace GameStore.api.Dtos;

public record class UpdateGameDto(
    [Required][StringLength(60)] string Name,
    [Required][StringLength(25)]string Genre,
    [Range(1, 110)]decimal Price,
    DateOnly ReleaseDate
);
