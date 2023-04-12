namespace DraftSimulator;

public interface IPlayerClient
{
    public Task<Player> GetPlayer(int playerId);

}
