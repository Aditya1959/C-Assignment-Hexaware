using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBookingSystem.entity
{
    public class Venue
    {
        // Attributes
        public string VenueName { get; set; }
        public string Address { get; set; }

        // Default constructor
        public Venue() { }

        // Overloaded constructor
        public Venue(string venueName, string address)
        {
            VenueName = venueName;
            Address = address;
        }

        // Method to display venue details
        public void DisplayVenueDetails()
        {
            Console.WriteLine($"Venue: {VenueName}, Address: {Address}");
        }
    }
}
