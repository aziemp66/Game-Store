using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GameStore.API.Dtos;

public record GameDto(
	 int Id,
	string Name,
	string Genre,
	decimal Price,
	DateTime ReleaseDate,
	string ImageUri
);

public record CreateGameDto(
	[Required][StringLength(50)] string Name,
	[Required][StringLength(20)] string Genre,
	[Range(0, 1000)] decimal Price,
	DateTime ReleaseDate,
	[Url][StringLength(250)] string ImageUri
);

public record UpdateGameDto(
	[Required] int Id,
	[Required][StringLength(50)] string Name,
	[Required][StringLength(20)] string Genre,
	[Range(0, 1000)] decimal Price,
	DateTime ReleaseDate,
	[Url][StringLength(250)] string ImageUri
);

