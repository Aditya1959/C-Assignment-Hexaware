using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using TicketBookingSystem.entity;

using System;
using System.Collections.Generic;

namespace TicketBookingSystem
{
    public class Booking
    {
        private static int bookingIdCounter = 1;
        public int BookingId { get; private set; }
        public List<Customer> Customers { get; private set; }
        public Event Event { get; private set; }
        public int NumberOfTickets { get; private set; }
        public decimal TotalCost { get; private set; }
        public DateTime BookingDate { get; private set; }

        public Booking(Event eventObj, List<Customer> customers, int numberOfTickets, decimal totalCost)
        {
            BookingId = bookingIdCounter++;
            Event = eventObj;
            Customers = customers;
            NumberOfTickets = numberOfTickets;
            TotalCost = totalCost;
            BookingDate = DateTime.Now;
        }

        public void DisplayBookingDetails()
        {
            Console.WriteLine($"Booking ID: {BookingId}");
            Console.WriteLine($"Event: {Event.EventName}");
            Console.WriteLine($"Number of Tickets: {NumberOfTickets}");
            Console.WriteLine($"Total Cost: {TotalCost:C}");
            Console.WriteLine($"Booking Date: {BookingDate}");
        }
    }
}

