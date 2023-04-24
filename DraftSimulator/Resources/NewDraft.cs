namespace DraftSimulator;

public class NewDraft
{
    public required string Name { get; set;}

    public required string Type { get; set;}

    public required int NumPlayers { get; set;}

    public string[]? DraftTeams { get; set; }
}
