using System.Text.Json.Serialization;

namespace PlayerService;

public class NhlTeam
{
    public List<Team>? Teams { get; set; }
}

public class NhlRoster
{
    [JsonPropertyName("roster")]
    public List<NhlPlayer>? Roster { get; set; }
}

public class NhlPlayer
{
    [JsonPropertyName("person")]
    public NhlPlayerInfo? Player { get; set; } 
}

public class NhlPlayerInfo
{
    public int id { get; set; }
}

public class NhlPlayers
{
    [JsonPropertyName("people")]
    public List<Player>? Players { get; set; }
}