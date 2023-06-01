using Flights.ReadModels;
using Microsoft.AspNetCore.Mvc;

namespace Flights.Controllers;

[ApiController]
[Route("[controller]")]
public class FlightController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<FlightController> _logger;

    public FlightController(ILogger<FlightController> logger)
    {
        _logger = logger;
    }
    
    Random random = new Random();
    [HttpGet]
    public IEnumerable<FlightRm> Search()
    {
        return new FlightRm[]
        {
            new (Guid.NewGuid(), 
                "Deutsche BA",
                random.Next(90,5000).ToString(),
                new TimePlaceRm("Munchen", DateTime.Now.AddHours(
                    random.Next(1,10))),
                new TimePlaceRm("Schiphol", DateTime.Now.AddHours(
                    random.Next(4,15))),
                random.Next(1,853)
                ),
            new (Guid.NewGuid(), 
                "Deutscheasdas BA",
                random.Next(90,5000).ToString(),
                new TimePlaceRm("Munchen", DateTime.Now.AddHours(
                    random.Next(1,10))),
                new TimePlaceRm("Schiphol", DateTime.Now.AddHours(
                    random.Next(4,15))),
                random.Next(1,853)
            )
        };
    }
}