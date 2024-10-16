using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBookingSystem.exception
{
    public class CustomException
    {
        public class EventNotFoundException : Exception
        {
            public EventNotFoundException(string message) : base(message) { }
        }

        // Custom exception for Invalid Booking ID
        public class InvalidBookingIDException : Exception
        {
            public InvalidBookingIDException(string message) : base(message) { }
        }
    }
}
