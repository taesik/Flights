namespace Flights.Domain.Entities;

public record User(
    string Email,
    string FirstName,
    string LastName,
    bool Gender
);