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


    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<User>().HasKey(p => p.Email);

        mb.Entity<Flight>().Property(p => p.RemainingNumberOfSeats)
            .IsConcurrencyToken();
        mb.Entity<Flight>().OwnsOne(f => f.Departure);
        mb.Entity<Flight>().OwnsOne(f => f.Arrival);
    }
}