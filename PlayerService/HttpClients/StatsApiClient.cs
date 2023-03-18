using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PlayerService
{
    public class StatsApiClient : IStatsApiClient
    {
        private readonly string _teamsPath = "/api/v1/teams";
        private readonly string _rosterPath = "/api/v1/teams/{0}/roster";
        private readonly string _playerPath = "/api/v1/people/{0}";
        private readonly HttpClient _httpClient;

        public StatsApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Team>> GetTeams()
        {
            var response = await _httpClient.GetAsync(_teamsPath);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to retrieve teams: {response.ReasonPhrase}");
            }

            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            var rootObject = JsonSerializer.Deserialize<NhlTeam>(content, options);
            if (rootObject is null ||
                rootObject.Teams is null)
            {
                throw new Exception("Failed to deserialize root object or its teams list is null.");
            }

            return rootObject.Teams;
        }

        public async Task<List<int>> GetPlayers(int teamId)
        {
            var response = await _httpClient.GetAsync(string.Format(_rosterPath, teamId));
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            var root = JsonSerializer.Deserialize<NhlRoster>(jsonString);

            if (root is null ||
                root.Roster is null)
            {
                throw new Exception("Invalid roster returned from NHL API.");
            }

            List<int> ids = root.Roster.Select(
                player => 
                {
                    if (player is null ||
                        player.Player is null)
                    {
                        throw new Exception("Invaild player returned from NHL API.");
                    }
                    
                    return (int)player.Player.id;
                }).ToList();
            
            return ids;
        }

        public async Task<Player> GetPlayerInfo(int playerId)
        {
            var response = await _httpClient.GetAsync(string.Format(_playerPath, playerId));
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var root = JsonSerializer.Deserialize<NhlPlayers>(content, options);

            if (root is null ||
                root.Players is null ||
                root.Players.Count == 0)
            {
                throw new Exception("Failed to get player information.");
            }

            return root.Players.First();
        }
    }
}
