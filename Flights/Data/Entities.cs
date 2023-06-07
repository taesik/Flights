using Flights.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Flights.Data;

public class Entities : DbContext
{
    public DbSet<User> Users => Set<User>();

    public DbSet<Flight> Flights => Set<Flight>();

    public Entities(DbContextOptions<Entities> options) : base(options)
    {
    }
}