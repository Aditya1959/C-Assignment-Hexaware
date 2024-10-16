using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBookingSystem.entity
{
    public class MovieConcrete : EventAbstract
    {
        public string Genre { get; set; }
        public string ActorName { get; set; }
        public string ActressName { get; set; }

        public MovieConcrete(string eventName, DateTime eventDate, TimeSpan eventTime, string venueName, int totalSeats, decimal ticketPrice, string genre, string actorName, string actressName)
        {
            EventName = eventName;
            EventDate = eventDate;
            EventTime = eventTime;
            VenueName = venueName;
            TotalSeats = totalSeats;
            AvailableSeats = totalSeats;
            TicketPrice = ticketPrice;
            EventType = "Movie";
            Genre = genre;
            ActorName = actorName;
            ActressName = actressName;
        }

        public override void DisplayEventDetails()
        {
            Console.WriteLine($"Movie: {EventName}, Date: {EventDate}, Time: {EventTime}, Venue: {VenueName}, Available Seats: {AvailableSeats}, Ticket Price: {TicketPrice:C}, Genre: {Genre}, Actor: {ActorName}, Actress: {ActressName}");
        }
    }
}
