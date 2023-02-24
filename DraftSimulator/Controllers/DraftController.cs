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
}