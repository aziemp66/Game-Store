using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.API.Entities
{
    public class Game
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Genre { get; set; }
        public required decimal Price { get; set; }
        public required DateTime ReleaseDate { get; set; }
        public required string ImageUri { get; set; }
    }
}