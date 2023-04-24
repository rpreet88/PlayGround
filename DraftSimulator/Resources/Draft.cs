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
        Name = newDraft.Name;
        Type = newDraft.Type;
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

    public Draft() {}
    
    public System.Guid DraftId { get; set; } = new Guid();

    public string Name { get; set; } = "";

    public string Type { get; set;} = "";

    public int NumPlayers { get; set; } = 0;

    public List<DraftTeam>? DraftTeams { get; set; }
}
