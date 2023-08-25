using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace GameStore.API.Entities;

public class Game
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public required string Name { get; set; }

    [Required]
    [StringLength(20)]
    public required string Genre { get; set; }

    [Required]
    [Range(0, 1000)]
    public required decimal Price { get; set; }

    [Required]
    public required DateTime ReleaseDate { get; set; }

    [Url]
    [StringLength(250)]
    public required string ImageUri { get; set; }
}