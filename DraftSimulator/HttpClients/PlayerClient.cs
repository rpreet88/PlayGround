using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DraftSimulator;

public class PlayerClient : IPlayerClient
{
    private readonly string _playerPath = "/player/{0}";
    private readonly HttpClient _httpClient;

    public PlayerClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Player> GetPlayer(int playerId)
    {
        var response = await _httpClient.GetAsync(String.Format(_playerPath, playerId));
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to retrieve teams: {response.ReasonPhrase}");
        }

        var content = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        var player = JsonSerializer.Deserialize<Player>(content, options);
        if (player is null)
        {
            throw new Exception("Failed to deserialize root object or its teams list is null.");
        }

        return player;
    }
}

