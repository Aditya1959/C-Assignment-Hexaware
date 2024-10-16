using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

using System;

namespace TicketBookingSystem
{
    public class Venue
    {
        public string VenueName { get; set; }
        public string Address { get; set; }
        public int VenueId { get; internal set; }

        public Venue(string venueName, string address)
        {
            VenueName = venueName;
            Address = address;
        }

        public void DisplayVenueDetails()
        {
            Console.WriteLine($"Venue: {VenueName}, Address: {Address}");
        }
    }
}
