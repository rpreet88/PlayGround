namespace DraftSimulator;

public class DraftTeamEntity : BaseEntity
{
    public DraftTeamEntity()
    {
        Players = new HashSet<PlayerEntity>();
    }

    public required Guid DraftId { get; set; }

    public required string Name { get; set; }

    public required int NumPlayers { get; set; }

    public virtual DraftEntity? Draft { get; set; } = null!;

    public virtual ICollection<PlayerEntity> Players { get; set; }
}
