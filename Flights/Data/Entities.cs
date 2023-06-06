using Flights.Domain.Entities;

namespace Flights.Data;

public class Entities
{
    public IList<User> Users = new List<User>();
    static Random random = new Random();

    public Flight[] Flights = new Flight[]
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
}