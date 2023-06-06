using Flights.Domain.Entities;
using Flights.Domain.Errors;
using Flights.Dtos;
using Flights.ReadModels;
using Microsoft.AspNetCore.Mvc;

namespace Flights.Controllers;

[ApiController]
[Route("[controller]")]
public class FlightController : ControllerBase
{
    private readonly ILogger<FlightController> _logger;

    static Random random = new Random();

    static private Flight[] flights = new Flight[]
    {
        new(Guid.NewGuid(),
            "Deutsche BA",
            random.Next(90, 5000).ToString(),
            new TimePlace("Munchen", DateTime.Now.AddHours(
                random.Next(1, 10))),
            new TimePlace("Schiphol", DateTime.Now.AddHours(
                random.Next(4, 15))),
            2
        ),
        new(Guid.NewGuid(),
            "Deutscheasdas BA",
            random.Next(90, 5000).ToString(),
            new TimePlace("Munchen", DateTime.Now.AddHours(
                random.Next(1, 10))),
            new TimePlace("Schiphol", DateTime.Now.AddHours(
                random.Next(4, 15))),
            random.Next(1, 853)
        )
    };


    public FlightController(ILogger<FlightController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [ProducesResponseType(typeof(FlightRm), 200)]
    public IEnumerable<FlightRm> Search()
    {
        var fRm = flights.Select(flight => new FlightRm(
            flight.Id,
            flight.Airline,
            flight.Price,
            new TimePlaceRm(flight.Departure.Place.ToString(),
                flight.Departure.Time
            ),
            new TimePlaceRm(flight.Arrival.Place.ToString(),
                flight.Arrival.Time
            ),
            flight.RemainingNumberOfSeats
        )).ToArray();

        return fRm;
    }


    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [ProducesResponseType(typeof(FlightRm), 200)]
    [HttpGet("{id}")]
    public ActionResult<FlightRm> Find(Guid id)
    {
        var flight = flights.SingleOrDefault(f => f.Id == id);

        if (flight == null) return NotFound();

        var readModel = new FlightRm(
            flight.Id,
            flight.Airline,
            flight.Price,
            new TimePlaceRm(
                flight.Departure.Place.ToString(),
                flight.Departure.Time
            ), new TimePlaceRm(
                flight.Arrival.Place.ToString(),
                flight.Arrival.Time
            ),
            flight.RemainingNumberOfSeats
        );

        return Ok(readModel);
    }

    [HttpPost]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [ProducesResponseType(200)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Book(BookDto dto)
    {
        System.Diagnostics.Debug.WriteLine(
            $"Booking a new flight {dto.FlightId}"
        );

        var flight = flights.SingleOrDefault(f => f.Id == dto.FlightId);

        if (flight == null) return NotFound();

        var err = flight.MakeBooking(dto.PassengerEmail, dto.NumberOfSeats);

        if (err is OverbookError)
            return Conflict(new { message = "Not enough seats" });

        return CreatedAtAction(nameof(Find), new { id = dto.FlightId });
    }
}