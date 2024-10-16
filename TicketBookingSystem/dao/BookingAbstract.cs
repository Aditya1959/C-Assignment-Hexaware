using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.entity;

namespace TicketBookingSystem.dao
{
    public abstract class BookingAbstract
    {
        public abstract Event CreateEvent(string eventName, DateTime eventDate, TimeSpan eventTime, string venueName, int totalSeats, decimal ticketPrice, string eventType);
        public abstract void BookTickets(Event eventToBook, int numTickets);
        public abstract int GetAvailableSeats(Event eventToCheck);
        public abstract void CancelTickets(Event eventToCancel, int numTickets);
    }
}
