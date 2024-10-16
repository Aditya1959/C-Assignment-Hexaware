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
    public class Concert : Event
    {
        private string v;

        public string Artist { get; set; }
        public string Type { get; set; }

        public Concert(string eventName, DateTime eventDate, TimeSpan eventTime, Venue venue, int totalSeats, decimal ticketPrice, string artist, string type)
            : base(eventName, eventDate, eventTime, venue, totalSeats, ticketPrice)
        {
            Artist = artist;
            Type = type;
            EventType = "Concert";
        }

        public Concert(string eventName, DateTime eventDate, TimeSpan eventTime, Venue venue, int totalSeats, decimal ticketPrice, string v) : base(eventName, eventDate, eventTime, venue, totalSeats, ticketPrice)
        {
            this.v = v;
        }

        public override void DisplayEventDetails()
        {
            Console.WriteLine($"Concert: {EventName}, Artist: {Artist}, Type: {Type}");
            Console.WriteLine($"Date: {EventDate.ToShortDateString()}, Time: {EventTime}, Venue: {Venue.VenueName}");
            Console.WriteLine($"Available Seats: {AvailableSeats}");
        }
    }
}
