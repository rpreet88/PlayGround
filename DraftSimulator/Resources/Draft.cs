namespace DraftSimulator;

public class Draft
{
    public Draft(NewDraft newDraft)
    {
        if (newDraft is null || 
            newDraft.DraftTeams is null)
        {
            throw new Exception("Invalid new draft input.");
        }

        DraftId = Guid.NewGuid();
        Name = newDraft.Name ?? "NoName";
        Type = newDraft.Type ?? "NoType";
        NumPlayers = newDraft.NumPlayers;

        DraftTeams = new List<DraftTeam>();
        foreach (string teamName in newDraft.DraftTeams)
        {
            DraftTeam team = new DraftTeam
            {
                TeamId = Guid.NewGuid(),
                Name = teamName
            };
            DraftTeams.Add(team);
        }
    }
    
    public System.Guid DraftId { get; }

    public string Name { get; set; }

    public string Type { get; }

    public int NumPlayers { get; set; }

    public List<DraftTeam> DraftTeams { get; set; }
}
