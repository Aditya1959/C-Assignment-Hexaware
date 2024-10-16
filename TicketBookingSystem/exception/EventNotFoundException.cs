using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace TicketBookingSystem
{
    public class EventNotFoundException : Exception
    {
        public EventNotFoundException(string message) : base(message) { }
    }
}

