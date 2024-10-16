using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.entity;

using System;
using System.Collections.Generic;
using TicketBookingSystem.dao;

namespace TicketBookingSystem
{
    public class BookingSystemServiceProviderImpl : IBookingSystemServiceProvider
    {
        private EventServiceProviderImpl eventServiceProvider = new EventServiceProviderImpl();
        private IBookingSystemRepository bookingRepository = new BookingSystemRepositoryImpl();

        public void CreateEvent()
        {
            Console.WriteLine("Enter event name:");
            string eventName = Console.ReadLine();

            Console.WriteLine("Enter event date (yyyy-mm-dd):");
            DateTime eventDate = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Enter event time (hh:mm):");
            TimeSpan eventTime = TimeSpan.Parse(Console.ReadLine());

            Console.WriteLine("Enter venue name:");
            string venueName = Console.ReadLine();

            Console.WriteLine("Enter venue address:");
            string address = Console.ReadLine();

            Venue venue = new Venue(venueName, address);

            Console.WriteLine("Enter total seats:");
            int totalSeats = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter ticket price:");
            decimal ticketPrice = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Enter event type (Movie, Concert, Sport):");
            string eventType = Console.ReadLine();

            eventServiceProvider.CreateEvent(eventName, eventDate, eventTime, venue, totalSeats, ticketPrice, eventType); // Task 1
        }

        public void BookTickets()
        {
            Console.WriteLine("Enter event ID:");
            int eventId = int.Parse(Console.ReadLine());
            Event eventObj = eventServiceProvider.GetEventDetails(eventId); // Task 5
            Console.WriteLine(eventObj.EventId);
            Console.WriteLine("Enter number of tickets:");
            int numberOfTickets = int.Parse(Console.ReadLine());

            if (numberOfTickets > eventObj.AvailableSeats)
            {
                Console.WriteLine("Not enough tickets available.");
                return;
            }

            List<Customer> customers = new List<Customer>();
            for (int i = 0; i < numberOfTickets; i++)
            {
                Console.WriteLine("Enter customer name:");
                string name = Console.ReadLine();

                Console.WriteLine("Enter customer email:");
                string email = Console.ReadLine();

                Console.WriteLine("Enter customer phone:");
                string phone = Console.ReadLine();

                customers.Add(new Customer(name, email, phone));
            }

            decimal totalCost = numberOfTickets * eventObj.TicketPrice;
            Booking booking = new Booking(eventObj, customers, numberOfTickets, totalCost);
            bookingRepository.SaveBooking(booking); // Save booking to DB
            eventObj.BookTickets(numberOfTickets);

            Console.WriteLine("Booking successful!");
            booking.DisplayBookingDetails(); // Display booking details
        }

        public void CancelTickets()
        {
            Console.WriteLine("Enter booking ID:");
            int bookingId = int.Parse(Console.ReadLine());
            Booking booking = bookingRepository.GetBooking(bookingId); // Task 3

            if (booking != null)
            {
                booking.Event.CancelBooking(booking.NumberOfTickets);
                bookingRepository.CancelBooking(bookingId); // Cancel booking in DB
                Console.WriteLine("Booking cancelled successfully!");
            }
            else
            {
                Console.WriteLine("Booking not found.");
            }
        }

        public void GetAvailableSeats()
        {
            Console.WriteLine("Enter event ID:");
            int eventId = int.Parse(Console.ReadLine());
            int availableSeats = eventServiceProvider.GetAvailableNoOfTickets(eventId); // Task 4
            Console.WriteLine($"Available seats for event ID {eventId}: {availableSeats}");
        }

        public void GetEventDetails()
        {
            Console.WriteLine("Enter event ID:");
            int eventId = int.Parse(Console.ReadLine());
            Event eventDetails = eventServiceProvider.GetEventDetails(eventId); // Task 5
            eventDetails.DisplayEventDetails(); // Display event details
        }
    }
}

