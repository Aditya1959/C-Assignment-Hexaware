using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.entity;

namespace TicketBookingSystem
{
    public interface IBookingSystemServiceProvider
    {
        void CreateEvent();
        void BookTickets();
        void CancelTickets();
        void GetAvailableSeats();
        void GetEventDetails();
    }
}

