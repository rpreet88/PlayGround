using Microsoft.AspNetCore.Mvc;

namespace DraftSimulator.Controllers;

[ApiController]
[Route("[controller]")]
public class DraftController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;

    public DraftController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<Draft> Get()
    {
        return new Draft{
            Name = "DefaultDraft",
            Type = "DefaultType"
        };
    }
}
