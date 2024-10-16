using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.dao;
using TicketBookingSystem.entity;
using static TicketBookingSystem.exception.CustomException;

namespace TicketBookingSystem.main
{
    public class TicketBookingSystem
    {
        public Event CreateEvent(string eventName, DateTime date, TimeSpan time, int totalSeats, decimal ticketPrice, string eventType, string venueName, string genre = null, string actorName = null, string actressName = null, string artist = null, string type = null, string sportName = null, string teamsName = null)
        {
            Event newEvent = null;

            switch (eventType.ToLower())
            {
                case "movie":
                    newEvent = new Movie(eventName, date, time, venueName, totalSeats, ticketPrice, eventType, genre, actorName, actressName);
                    break;
                case "concert":
                    newEvent = new Concert(eventName, date, time, venueName, totalSeats, ticketPrice, eventType, artist, type);
                    break;
                case "sport":
                    newEvent = new Sports(eventName, date, time, venueName, totalSeats, ticketPrice, eventType, sportName, teamsName);
                    break;
                default:
                    Console.WriteLine("Invalid event type.");
                    break;
            }

            return newEvent;
        }

        public void DisplayEventDetails(Event eventObj)
        {
            eventObj.DisplayEventDetails();
        }

        public decimal BookTickets(Event eventObj, int numTickets)
        {
            if (eventObj.BookTickets(numTickets))
            {
                return eventObj.CalculateTotalRevenue(numTickets);
            }
            else
            {
                Console.WriteLine("Not enough available seats.");
                return 0;
            }
        }

        public void CancelTickets(Event eventObj, int numTickets)
        {
            eventObj.CancelBooking(numTickets);
            Console.WriteLine($"Cancelled {numTickets} tickets.");
        }

        public void Main()
        {
            // Sample Usage
            var ticketSystem = new TicketBookingSystem();
            Event movieEvent = ticketSystem.CreateEvent("Avengers: Endgame", DateTime.Now.AddDays(30), new TimeSpan(20, 0, 0), 500, 200.00m, "movie", "IMAX", "Action", "Robert Downey Jr.", "Scarlett Johansson");

            ticketSystem.DisplayEventDetails(movieEvent);
            decimal totalCost = ticketSystem.BookTickets(movieEvent, 3);
            Console.WriteLine($"Total cost: {totalCost:C}");

            // Cancel tickets
            ticketSystem.CancelTickets(movieEvent, 1);
            ticketSystem.DisplayEventDetails(movieEvent);
        }

        private List<Booking> bookings = new List<Booking>();
        private IEventServiceProvider eventServiceProvider;

        public TicketBookingSystem(IEventServiceProvider eventServiceProvider)
        {
            this.eventServiceProvider = eventServiceProvider;
        }

        public TicketBookingSystem()
        {
        }

        // Book Tickets Method (Throws EventNotFoundException)
        public void BookTickets(string eventName, int numTickets)
        {
            Event selectedEvent = FindEvent(eventName);
            if (selectedEvent == null)
            {
                throw new EventNotFoundException($"Event '{eventName}' not found.");
            }

            if (!selectedEvent.BookTickets(numTickets))
            {
                Console.WriteLine("Not enough available tickets.");
            }
            else
            {
                Console.WriteLine($"{numTickets} tickets booked successfully for {eventName}.");
            }
        }

        // Method to Find Event by Event Name
        private Event FindEvent(string eventName)
        {
            Event[] events = eventServiceProvider.GetEventDetails();
            foreach (var ev in events)
            {
                if (ev.EventName.Equals(eventName, StringComparison.OrdinalIgnoreCase))
                {
                    return ev;
                }
            }
            return null;
        }

        // Method to Cancel Booking (Throws InvalidBookingIDException)
        public void CancelBooking(int bookingId)
        {
            Booking booking = bookings.Find(b => b.BookingID == bookingId);
            if (booking == null)
            {
                throw new InvalidBookingIDException($"Booking ID '{bookingId}' is invalid.");
            }

            Event bookingEvent = (Event)booking.Event;
            bookingEvent.CancelBooking(booking.NumTickets);
            bookings.Remove(booking);
            Console.WriteLine($"Booking with ID {bookingId} has been successfully cancelled.");
        }

        // View Booking Details (Throws InvalidBookingIDException)
        public void ViewBookingDetails(int bookingId)
        {
            Booking booking = bookings.Find(b => b.BookingID == bookingId);
            if (booking == null)
            {
                throw new InvalidBookingIDException($"Booking ID '{bookingId}' is invalid.");
            }

            Console.WriteLine($"Booking ID: {booking.BookingID}");
            Console.WriteLine($"Event: {booking.Event.EventName}");
            Console.WriteLine($"Number of Tickets: {booking.NumTickets}");
        }
    }
}
}
