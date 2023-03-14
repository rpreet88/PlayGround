using Microsoft.AspNetCore.Mvc;
using System;

namespace DraftSimulator.Controllers;

[ApiController]
[Route("[controller]")]
public class DraftController : ControllerBase
{
    private readonly ILogger<DraftController> _logger;
    private DraftStore _draftStore;

    public DraftController(
        DraftStore draftStore,
        ILogger<DraftController> logger)
    {
        _draftStore = draftStore;
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
        Team? team = draft.Teams.FirstOrDefault(t => t.TeamId == teamId);
        if (team == null)
        {
            return NotFound();
        }
        return Ok(team);
    }

    [HttpPost("{draftId}/team/{teamId}/player")]
    public IActionResult Add(Guid draftId, Guid teamId, [FromBody] string playerId)
    {
        Player player = new Player
        {
            Name = playerId
        };

        _draftStore.AddPlayer(draftId, teamId, player);
        return Ok();
    }
}