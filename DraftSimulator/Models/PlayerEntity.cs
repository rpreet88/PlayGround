namespace DraftSimulator;

public class PlayerEntity : BaseEntity
{
    public required string FullName { get; set; }

    public required int PrimaryNumber { get; set; }

    public required int CurrentAge { get; set; }

    public required string ShootsCatches { get; set; }

    public required TeamEntity Team { get; set; }
     
    public required PositionEntity Position { get; set; }

    public required Guid DraftTeamId { get; set; }

    public virtual DraftTeamEntity? DraftTeam { get; set; }
    
}

public class TeamEntity : BaseEntity
{
    public required Guid PlayerId { get; set; }

    public required string Name { get; set; }

    public required string Link { get; set; }

    public required string LocationName { get; set; }  

    public virtual PlayerEntity? Player { get; set; } 
}

public class PositionEntity : BaseEntity
{
    public required Guid PlayerId { get; set; }
    
    public required string Code { get; set; }

    public required string Name { get; set; }

    public required string Type { get; set; }

    public virtual PlayerEntity? Player { get; set; }
}