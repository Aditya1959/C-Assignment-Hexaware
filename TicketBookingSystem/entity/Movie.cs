using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TicketBookingSystem.entity;

using System;

namespace TicketBookingSystem
{
    public class Movie : Event
    {
        public string Genre { get; set; }
        public string ActorName { get; set; }
        public string ActressName { get; set; }

        public Movie(string eventName, DateTime eventDate, TimeSpan eventTime, Venue venue, int totalSeats, decimal ticketPrice, string genre, string actorName, string actressName)
            : base(eventName, eventDate, eventTime, venue, totalSeats, ticketPrice)
        {
            Genre = genre;
            ActorName = actorName;
            ActressName = actressName;
            EventType = "Movie";
        }

        public override void DisplayEventDetails()
        {
            Console.WriteLine($"Movie: {EventName}, Genre: {Genre}, Actor: {ActorName}, Actress: {ActressName}");
            Console.WriteLine($"Date: {EventDate.ToShortDateString()}, Time: {EventTime}, Venue: {Venue.VenueName}");
            Console.WriteLine($"Available Seats: {AvailableSeats}");
        }
    }
}
