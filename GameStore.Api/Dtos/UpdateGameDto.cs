using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

public record   UpdateGameDto
(
  [Required ][StringLength(40)] string Name,
     [Required][StringLength(20)] string Genre,
    [Range(1, 100)]decimal Price,
    DateOnly  ReleaseDate
);
