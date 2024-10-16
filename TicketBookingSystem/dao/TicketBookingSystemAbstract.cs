using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.entity;

namespace TicketBookingSystem.dao
{
    public class TicketBookingSystemAbstract : BookingAbstract
    {
        private List<Event> events = new List<Event>();

        public override Event CreateEvent(string eventName, DateTime eventDate, TimeSpan eventTime, string venueName, int totalSeats, decimal ticketPrice, string eventType)
        {
            Event newEvent = null;

            switch (eventType.ToLower())
            {
                case "movie":
                    newEvent = new MovieConcrete(eventName, eventDate, eventTime, venueName, totalSeats, ticketPrice, "GenreExample", "ActorExample", "ActressExample");
                    break;
                case "concert":
                    newEvent = new Concert(eventName, eventDate, eventTime, venueName, totalSeats, ticketPrice, "ArtistExample", "TypeExample");
                    break;
                case "sports":
                    newEvent = new Sports(eventName, eventDate, eventTime, venueName, totalSeats, ticketPrice, "SportNameExample", "Team1 vs Team2");
                    break;
                default:
                    Console.WriteLine("Invalid event type.");
                    break;
            }

            if (newEvent != null)
            {
                events.Add(newEvent);
            }

            return newEvent;
        }

        public override void BookTickets(Event eventToBook, int numTickets)
        {
            if (eventToBook.AvailableSeats >= numTickets)
            {
                eventToBook.AvailableSeats -= numTickets;
                Console.WriteLine($"{numTickets} tickets booked for {eventToBook.EventName}. Remaining seats: {eventToBook.AvailableSeats}");
            }
            else
            {
                Console.WriteLine("Not enough available seats.");
            }
        }

        public override int GetAvailableSeats(Event eventToCheck)
        {
            return eventToCheck.AvailableSeats;
        }

        public override void CancelTickets(Event eventToCancel, int numTickets)
        {
            eventToCancel.AvailableSeats += numTickets;
            Console.WriteLine($"{numTickets} tickets canceled for {eventToCancel.EventName}. Available seats: {eventToCancel.AvailableSeats}");
        }
    }
}
