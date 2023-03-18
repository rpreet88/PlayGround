namespace PlayerService;

public class PlayerStore : IPlayerStore
{

    private readonly ILogger<PlayerStore> _logger;
    private readonly IStatsApiClient _statsApiClient;
    private List<Player> _playerStore;
    private bool _bInitiated;

    public PlayerStore(
        IStatsApiClient statsApiClient,
        ILogger<PlayerStore> logger)
    {
        _logger = logger;
        _statsApiClient = statsApiClient;
        _playerStore = new List<Player>();
        _bInitiated = false;
    }

    public Player GetPlayer(int playerId)
    {
        if (_playerStore is null)
        {
            throw new Exception("Invalid player store.");
        }

        var player = _playerStore.Find(p => p?.Id == playerId);

        if (player is null)
        {
            throw new Exception("Player not found.");
        }

        return player;
    }

    public List<Player> GetPlayers()
    {
        return _playerStore;
    }

    public async void InitiatePlayers()
    {
        if (_bInitiated)
        {
            return;
        }

        var teams = await _statsApiClient.GetTeams();
        List<int> playerIds = new List<int>();
        foreach(var team in teams)
        {
            var ids = await _statsApiClient.GetPlayers(team.Id);
            playerIds.AddRange(ids);
        }

        foreach(var playerId in playerIds)
        {
            var player = await _statsApiClient.GetPlayerInfo(playerId);
            _playerStore.Add(player);
        }

        _bInitiated = true;
    }
}
