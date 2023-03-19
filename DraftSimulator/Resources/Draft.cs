namespace DraftSimulator;

public class Draft
{
    public Draft(NewDraft newDraft)
    {
        if (newDraft is null || 
            newDraft.Teams is null)
        {
            throw new Exception("Invalid new draft input.");
        }

        DraftId = Guid.NewGuid();
        Name = newDraft.Name ?? "NoName";
        Type = newDraft.Type ?? "NoType";
        NumPlayers = newDraft.NumPlayers;

        Teams = new List<Team>();
        foreach (string teamName in newDraft.Teams)
        {
            Team team = new Team
            {
                TeamId = Guid.NewGuid(),
                Name = teamName
            };
            Teams.Add(team);
        }
    }
    
    public System.Guid DraftId { get; }

    public string Name { get; set; }

    public string Type { get; }

    public int NumPlayers { get; set; }

    public List<Team> Teams { get; set; }
}
