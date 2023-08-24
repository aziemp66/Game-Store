using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace GameStore.API.Entities
{
    public class Game
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        [Required]
        [StringLength(100)]
        public required string Name { get; set; }

        [JsonPropertyName("genre")]
        [Required]
        [StringLength(20)]
        public required string Genre { get; set; }

        [JsonPropertyName("price")]
        [Required]
        [Range(1, 1000)]
        public required decimal Price { get; set; }

        [JsonPropertyName("release_date")]
        [Required]
        public required DateTime ReleaseDate { get; set; }

        [JsonPropertyName("image_uri")]
        [Url]
        [StringLength(250)]
        public required string ImageUri { get; set; }
    }
}