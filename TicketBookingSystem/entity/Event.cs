using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using TicketBookingSystem.entity;

using System;

namespace TicketBookingSystem
{
    public abstract class Event
    {
        public int EventId { get; set; } // Added Event ID for DB reference
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public TimeSpan EventTime { get; set; }
        public Venue Venue { get; set; }
        public int TotalSeats { get; set; }
        public int AvailableSeats { get; set; }
        public decimal TicketPrice { get; set; }
        public string EventType { get; set; }

        public Event(string eventName, DateTime eventDate, TimeSpan eventTime, Venue venue, int totalSeats, decimal ticketPrice)
        {
            EventName = eventName;
            EventDate = eventDate;
            EventTime = eventTime;
            Venue = venue;
            TotalSeats = totalSeats;
            AvailableSeats = totalSeats;
            TicketPrice = ticketPrice;
        }

        public bool BookTickets(int numTickets)
        {
            if (AvailableSeats >= numTickets)
            {
                AvailableSeats -= numTickets; // Decrease available seats
                                              // Additional logic for booking...
                return true; // Booking successful
            }
            return false; // Not enough seats available
        }

        public void CancelBooking(int numberOfTickets)
        {
            AvailableSeats += numberOfTickets;
        }

        public abstract void DisplayEventDetails();
        public decimal CalculateTotalRevenue(int ticketsSold)
        {
            return ticketsSold * TicketPrice;
        }
    }
}
