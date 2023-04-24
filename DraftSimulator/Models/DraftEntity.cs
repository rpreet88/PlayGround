namespace DraftSimulator;

public class DraftEntity : BaseEntity
{
    public DraftEntity()
    {
        DraftTeams = new HashSet<DraftTeamEntity>();
    }
    
    public required string Name { get; set; }

    public required string Type { get; set; }

    public required int NumPlayers { get; set; }

    public virtual ICollection<DraftTeamEntity> DraftTeams { get; set; }
}
