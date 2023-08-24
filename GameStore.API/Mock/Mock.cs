using System.Text.Json;
using GameStore.API.Entities;

namespace GameStore.API.Mock
{
    public static class MockData
    {
        public static List<Game> GetGameList()
        {
            string mockDataLocation = "MockData/games.json";
            string mockData = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), mockDataLocation));

            List<Game> games = JsonSerializer.Deserialize<List<Game>>(mockData) ?? throw new Exception("Failed to deserialize mock data.");
            return games;
        }
    }
}