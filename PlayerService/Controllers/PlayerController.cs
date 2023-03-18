using Microsoft.AspNetCore.Mvc;
using System;

namespace PlayerService.Controllers;

[ApiController]
[Route("[controller]")]
public class PlayerController : ControllerBase
{
    private readonly ILogger<PlayerController> _logger;
    private PlayerStore _playerStore;

    public PlayerController(
        PlayerStore playerStore,
        ILogger<PlayerController> logger)
    {
        _logger = logger;
        _playerStore = playerStore;
    }

    [HttpGet("{id}")]
    public IActionResult GetPlayer(int id)
    {  
        var player = _playerStore.GetPlayer(id);
        return Ok(player);
    }

    [Route("/players")]
    public IActionResult GetPlayers(int id)
    {  
        var players = _playerStore.GetPlayers();
        return Ok(players);
    }
}