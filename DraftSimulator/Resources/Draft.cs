namespace DraftSimulator;

public class Draft
{
    public Draft(NewDraft newDraft)
    {
        DraftId = Guid.NewGuid();
        Name = newDraft.Name ?? "NoName";
        Type = newDraft.Type ?? "NoType";
        NumPlayers = newDraft.NumPlayers;
    }
    
    public System.Guid DraftId { get; }

    public string Name { get; set; }

    public string Type { get; }

    public int NumPlayers { get; set; }
}
