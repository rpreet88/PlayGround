namespace DraftSimulator;

public class Team
{
    public Team()
    {
        Players = new List<Player>();
    }

    public System.Guid TeamId { get; set; }

    public string? Name { get; set;}

    public List<Player> Players { get; set; }

    public int NumPlayers { get; set; }
}
