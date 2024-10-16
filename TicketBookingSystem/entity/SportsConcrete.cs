using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBookingSystem.entity
{
    public class SportsConcrete : EventAbstract
    {
        public string SportName { get; set; }
        public string TeamsName { get; set; }

        public SportsConcrete(string eventName, DateTime eventDate, TimeSpan eventTime, string venueName, int totalSeats, decimal ticketPrice, string sportName, string teamsName)
        {
            EventName = eventName;
            EventDate = eventDate;
            EventTime = eventTime;
            VenueName = venueName;
            TotalSeats = totalSeats;
            AvailableSeats = totalSeats;
            TicketPrice = ticketPrice;
            EventType = "Sports";
            SportName = sportName;
            TeamsName = teamsName;
        }

        public override void DisplayEventDetails()
        {
            Console.WriteLine($"Sports: {EventName}, Date: {EventDate}, Time: {EventTime}, Venue: {VenueName}, Available Seats: {AvailableSeats}, Ticket Price: {TicketPrice:C}, Sport: {SportName}, Teams: {TeamsName}");
        }
    }
}
