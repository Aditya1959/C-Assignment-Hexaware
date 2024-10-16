using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.entity;

using System.Collections.Generic;

namespace TicketBookingSystem
{
    public interface IBookingSystemRepository
    {
        void SaveBooking(Booking booking);
        Booking GetBooking(int bookingId);
        void CancelBooking(int bookingId);
        void SaveEvent(Event eventObj);
        Event GetEvent(int eventId);
    }
}
