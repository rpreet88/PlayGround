namespace DraftSimulator;

public class Player
{
    public int Id { get; set; }
    public string? FullName { get; set; }
    public string? PrimaryNumber { get; set; }
    public int? CurrentAge { get; set; }
    public string? ShootsCatches { get; set; }
    public Team? CurrentTeam { get; set; }
    public Position? PrimaryPosition { get; set; }
}

public class Team
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Link { get; set; }
    public string? LocationName  { get; set; }
}

public class Position
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? Type { get; set; }
}