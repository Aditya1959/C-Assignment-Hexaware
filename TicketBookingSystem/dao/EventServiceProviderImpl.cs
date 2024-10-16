using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.entity;


//using static TicketBookingSystem.exception.CustomException;
using TicketBookingSystem.dao;

namespace TicketBookingSystem
{
    public class EventServiceProviderImpl : IEventServiceProvider
    {
        private IBookingSystemRepository bookingRepository = new BookingSystemRepositoryImpl();

        public Event CreateEvent(string eventName, DateTime date, TimeSpan time, Venue venue, int totalSeats, decimal ticketPrice, string eventType)
        {
            Event newEvent = null;

            // Task 1: Creating different types of events
            if (eventType == "Movie")
            {
                newEvent = new Movie(eventName, date, time, venue, totalSeats, ticketPrice, "Action", "Actor Name", "Actress Name");
            }
            else if (eventType == "Concert")
            {
                newEvent = new Concert(eventName, date, time, venue, totalSeats, ticketPrice, "Artist Name", "Type");
            }
            else if (eventType == "Sport")
            {
                newEvent = new Sport(eventName, date, time, venue, totalSeats, ticketPrice, "Sport Name", "Team A vs Team B");
            }

            if (newEvent != null)
            {
                bookingRepository.SaveEvent(newEvent); // Save event to DB
                Console.WriteLine($"Event '{eventName}' created with ID: {newEvent.EventId}");
            }
            return newEvent;
        }

        public Event GetEventDetails(int eventId)
        {
            // Task 5: Fetch event details by ID
            Event eventObj = bookingRepository.GetEvent(eventId);
            if (eventObj == null)
            {
                throw new EventNotFoundException("Event not found.");
            }
            return eventObj;
        }

        public int GetAvailableNoOfTickets(int eventId)
        {
            // Task 4: Fetch available tickets for an event
            return GetEventDetails(eventId).AvailableSeats;
        }
    }
}
