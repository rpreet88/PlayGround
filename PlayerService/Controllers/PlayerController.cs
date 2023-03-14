using Microsoft.AspNetCore.Mvc;
using System;

namespace PlayerService.Controllers;

[ApiController]
[Route("[controller]")]
public class PlayerController : ControllerBase
{
    private readonly ILogger<PlayerController> _logger;

    public PlayerController(
        ILogger<PlayerController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public IActionResult Add()
    {
        return Ok();
    }

    [HttpGet("{id}")]
    public IActionResult Get(Guid id)
    {  
        return Ok();
    }
}