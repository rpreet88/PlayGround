using Microsoft.AspNetCore.Mvc;
using System;

namespace DraftSimulator.Controllers;

[ApiController]
[Route("[controller]")]
public class DraftController : ControllerBase
{
    private readonly ILogger<DraftController> _logger;
    private DraftStore _draftStore;
    private IPlayerClient _playerClient;

    public DraftController(
        DraftStore draftStore,
        IPlayerClient playerClient,
        ILogger<DraftController> logger)
    {
        _draftStore = draftStore;
        _playerClient = playerClient;
        _logger = logger;
    }

    [HttpPost]
    public IActionResult Add(NewDraft newDraft)
    {
        Draft draft = _draftStore.Add(newDraft);
        return Ok(draft.DraftId);
    }

    [HttpGet("{id}")]
    public IActionResult Get(Guid id)
    {  
        Draft draft = _draftStore.Get(id);
        return Ok(draft);
    }

    [HttpGet("{draftId}/team/{teamId}")]
    public IActionResult Get(Guid draftId, Guid teamId)
    {  
        Draft draft = _draftStore.Get(draftId);
        DraftTeam? team = draft.DraftTeams.FirstOrDefault(t => t.TeamId == teamId);
        if (team == null)
        {
            throw new TeamNotFound();
        }
        return Ok(team);
    }

    [HttpPost("{draftId}/team/{teamId}/player/{playerId}")]
    public async Task<IActionResult> Add(Guid draftId, Guid teamId, string playerId)
    {
        if (!int.TryParse(playerId, out int _playerId))
        {
            return BadRequest("Invalid player ID.");
        }

        Player player = 
            await _playerClient.GetPlayer(_playerId).ConfigureAwait(false);
        _draftStore.AddPlayer(draftId, teamId, player);
        return Ok();
    }
}