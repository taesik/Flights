﻿using Flights.Domain.Errors;
using Flights.ReadModels;

namespace Flights.Domain.Entities
{
    public class Flight
    {
        public Guid Id { get; set; }
        public string Airline { get; set; }
        public string Price { get; set; }
        public TimePlace Departure { get; set; }
        public TimePlace Arrival { get; set; }
        public int RemainingNumberOfSeats { get; set; }

        public IList<Booking> Bookings = new List<Booking>();

        public Flight()
        {
        }

        public Flight(
            Guid id,
            string airline,
            string price,
            TimePlace departure,
            TimePlace arrival,
            int remainingNumberOfSeats
        )
        {
            Id = id;
            Airline = airline;
            Price = price;
            Departure = departure;
            Arrival = arrival;
            RemainingNumberOfSeats = remainingNumberOfSeats;
        }


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

        public object? CancelBooking(string userEmail, byte numberOfSeats)
        {
            var booking = Bookings.FirstOrDefault(b =>
            {
                return numberOfSeats == b.NumberOfSeats
                       &&
                       userEmail.ToLower() == b.PassengerEmail.ToLower();
            });
            if (booking == null)
            {
                return new NotFoundError();
            }
            Bookings.Remove(booking);
            RemainingNumberOfSeats += booking.NumberOfSeats;


            return null;
        }
    }
}