using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.entity;
/*
namespace TicketBookingSystem.dao
{
    public class Booking
    {
        // Declare Event as a proper type
        private Event _event;
        public Event Event { get { return _event; } }

        // Public properties
        public int BookingID { get; internal set; }
        public int NumTickets { get; private set; }
        public string EventName { get; internal set; }
        public string CustomerName { get; internal set; }
        public DateTime BookingDate { get; internal set; }
        public decimal TotalCost { get; internal set; }
        public int BookingId { get; internal set; }

        private List<Customer> customers;

        // Constructor
        public Booking(int bookingID, Event eventObj, int numTickets, List<Customer> customerList)
        {
            BookingID = bookingID;
            _event = eventObj;
            NumTickets = numTickets;
            customers = customerList;  // Initialize the List
        }

        // Method to add customers to the booking
        public void AddCustomer(Customer customer)
        {
            customers.Add(customer);
        }

        // Method to display customer details
        public void DisplayCustomers()
        {
            foreach (var customer in customers)
            {
                customer.DisplayCustomerDetails();
            }
        }


        // Method to calculate booking cost
        public decimal CalculateBookingCost()
        {
            return _event.CalculateTotalRevenue(NumTickets);  // No need to pass numTickets, it's part of the object
        }

        // Method to book tickets
        public bool BookTickets()
        {
            return _event.BookTickets(NumTickets);  // Use NumTickets from this class
        }

        // Method to cancel booking
        public void CancelBooking()
        {
            _event.CancelBooking(NumTickets);  // Use NumTickets from this class
        }

        // Method to get available tickets
        public int GetAvailableNoOfTickets()
        {
            return _event.AvailableSeats;
        }

        // Method to display event details
        public void GetEventDetails()
        {
            _event.DisplayEventDetails();
        }
    }
}*/
