using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBookingSystem.entity
{
    public class ConcertConcrete : EventAbstract
    {
        public string Artist { get; set; }
        public string Type { get; set; }

        public ConcertConcrete(string eventName, DateTime eventDate, TimeSpan eventTime, string venueName, int totalSeats, decimal ticketPrice, string artist, string type)
        {
            EventName = eventName;
            EventDate = eventDate;
            EventTime = eventTime;
            VenueName = venueName;
            TotalSeats = totalSeats;
            AvailableSeats = totalSeats;
            TicketPrice = ticketPrice;
            EventType = "Concert";
            Artist = artist;
            Type = type;
        }

        public override void DisplayEventDetails()
        {
            Console.WriteLine($"Concert: {EventName}, Date: {EventDate}, Time: {EventTime}, Venue: {VenueName}, Available Seats: {AvailableSeats}, Ticket Price: {TicketPrice:C}, Artist: {Artist}, Type: {Type}");
        }
    }
}
