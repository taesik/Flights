using System.ComponentModel.DataAnnotations;

namespace Flights.Dtos;

public record BookDto(
    [Required] Guid FlightId,
    [Required] string PassengerEmail,
    [Required] byte NumberOfSeats
);