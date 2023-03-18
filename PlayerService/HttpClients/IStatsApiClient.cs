namespace PlayerService;

public interface IStatsApiClient
{
    Task<List<Team>> GetTeams();

    Task<List<int>> GetPlayers(int teamId);

    Task<Player> GetPlayerInfo(int playerId);
}
