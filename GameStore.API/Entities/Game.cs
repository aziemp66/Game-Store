using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace GameStore.API.Entities
{
    public class Game
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public required string Name { get; set; }
        [JsonPropertyName("genre")]
        public required string Genre { get; set; }
        [JsonPropertyName("price")]
        public required decimal Price { get; set; }
        [JsonPropertyName("release_date")]
        public required DateTime ReleaseDate { get; set; }
        [JsonPropertyName("image_uri")]
        public required string ImageUri { get; set; }
    }
}