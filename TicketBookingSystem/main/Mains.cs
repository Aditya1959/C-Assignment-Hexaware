using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using static TicketBookingSystem.exception.CustomException;
using TicketBookingSystem.dao;
using TicketBookingSystem.entity;

using System;

namespace TicketBookingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            BookingSystemServiceProviderImpl bookingSystem = new BookingSystemServiceProviderImpl();

            string command = "";
            while (command != "exit")
            {
                Console.WriteLine("Enter command (create_event, book_tickets, cancel_tickets, get_available_seats, get_event_details, exit):");
                command = Console.ReadLine();

                switch (command)
                {
                    case "create_event": // Task 1: Create an event
                        bookingSystem.CreateEvent();
                        break;

                    case "book_tickets": // Task 2: Book tickets
                        bookingSystem.BookTickets();
                        break;

                    case "cancel_tickets": // Task 3: Cancel tickets
                        bookingSystem.CancelTickets();
                        break;

                    case "get_available_seats": // Task 4: Get available seats
                        bookingSystem.GetAvailableSeats();
                        break;

                    case "get_event_details": // Task 5: Get event details
                        bookingSystem.GetEventDetails();
                        break;

                    case "exit":
                        Console.WriteLine("Exiting...");
                        break;

                    default:
                        Console.WriteLine("Invalid command. Try again.");
                        break;
                }
            }
        }
    }
}
