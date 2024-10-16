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
    public class Sport : Event
    {
        public string SportName { get; set; }
        public string TeamsName { get; set; }

        public Sport(string eventName, DateTime eventDate, TimeSpan eventTime, Venue venue, int totalSeats, decimal ticketPrice, string sportName, string teamsName)
            : base(eventName, eventDate, eventTime, venue, totalSeats, ticketPrice)
        {
            SportName = sportName;
            TeamsName = teamsName;
            EventType = "Sport";
        }

        public override void DisplayEventDetails()
        {
            Console.WriteLine($"Sport: {EventName}, Teams: {TeamsName}, Sport Type: {SportName}");
            Console.WriteLine($"Date: {EventDate.ToShortDateString()}, Time: {EventTime}, Venue: {Venue.VenueName}");
            Console.WriteLine($"Available Seats: {AvailableSeats}");
        }
    }
}


