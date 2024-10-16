using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.entity;

namespace TicketBookingSystem
{
    public interface IEventServiceProvider
    {
        Event CreateEvent(string eventName, DateTime date, TimeSpan time, Venue venue, int totalSeats, decimal ticketPrice, string eventType);
        Event GetEventDetails(int eventId);
        int GetAvailableNoOfTickets(int eventId);
    }
}

