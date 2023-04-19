namespace PlayerService;

public interface IPlayerStore
{
    Player GetPlayer(int playerId);
    List<Player> GetPlayers();
    void InitiatePlayers();
}

