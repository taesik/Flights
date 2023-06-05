namespace Flights.Dtos;

public record NewUserDto(
        string Email, 
        string FirstName,
        string LastName,
        bool Gender
    )
{
    
}