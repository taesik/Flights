﻿using Flights.Domain.Errors;
using Flights.ReadModels;

namespace Flights.Domain.Entities
{
    public record Flight(
        Guid Id,
        string Airline,
        string Price,
        TimePlace Departure,
        TimePlace Arrival,
        int RemainingNumberOfSeats
    )
    {
        public IList<Booking> Bookings = new List<Booking>();
        public int RemainingNumberOfSeats { get; set; } = RemainingNumberOfSeats;

        public object? MakeBooking(string userEmail, byte numberOfSeats)
        {
            var flight = this;

            if (flight.RemainingNumberOfSeats < numberOfSeats)
            {
                return new OverbookError();
            }

            flight.Bookings.Add(
                new Booking(
                    userEmail,
                    numberOfSeats
                )
            );
            flight.RemainingNumberOfSeats -= numberOfSeats;


            return null;
        }
    }
}