using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.entity;
using System.Data.SqlClient;


using System;
using System.Collections.Generic;
using System.Data.SqlClient;
//using static TicketBookingSystem.exception.CustomException;
/*
namespace TicketBookingSystem
{
    public class BookingSystemRepositoryImpl : IBookingSystemRepository
    {
        private string connectionString = "Data Source=DESKTOP-ULFNUFJ\\SQLEXPRESS;Initial Catalog=TicketBookingSystemNew;Integrated Security=True;"; // Replace with your DB connection string
        private Dictionary<int, Booking> bookings = new Dictionary<int, Booking>();
        private static int bookingIdCounter = 1;

        public int GetVenueIdByName(string venueName)
        {
            int venueId = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT VenueId FROM Venue WHERE VenueName = @VenueName";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@VenueName", venueName);

                    // Execute the query and get the VenueId
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        venueId = Convert.ToInt32(result);
                    }
                }
            }

            return venueId; // Return the retrieved VenueId
        }

        public int SaveVenueIfNotExists(Venue venue)
        {
            int venueId = GetVenueIdByName(venue.VenueName);
            if (venueId == 0) // Venue does not exist
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Venue (VenueName, Address) OUTPUT INSERTED.VenueId VALUES (@VenueName, @Address);";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@VenueName", venue.VenueName);
                        command.Parameters.AddWithValue("@Address", venue.Address);
                        venueId = Convert.ToInt32(command.ExecuteScalar());
                        // Log the newly inserted VenueId
                        Console.WriteLine($"New VenueId: {venueId}"); // Debugging line
                    }
                }
            }
            return venueId; // Return the VenueId
        }
        // Task 2: Save booking to DB
        public void SaveBooking(Booking booking)
        {
            Event eventObj = GetEvent(booking.Event.EventId);

            // Check if the event was fetched successfully
            if (eventObj == null)
            {
                throw new Exception("Event not found with ID: " + booking.Event.EventId);
            }
            // Log for debugging the EventId before saving
            Console.WriteLine($"Saving booking for EventId: {booking.Event.EventId}");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Bookings (EventId, TotalCost, NumberOfTickets) VALUES (@EventId, @TotalCost, @NumberOfTickets); SELECT SCOPE_IDENTITY();";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EventId", booking.Event.EventId);  // Ensure EventId is valid
                    command.Parameters.AddWithValue("@TotalCost", booking.TotalCost);
                    command.Parameters.AddWithValue("@NumberOfTickets", booking.NumberOfTickets);

                    // Log for debugging
                    Console.WriteLine($"Trying to save booking with EventId: {booking.Event.EventId}");

                    // Execute the command and get the booking ID
                    int bookingId = Convert.ToInt32(command.ExecuteScalar());
                    bookings[bookingId] = booking; // Save to local dictionary
                }
            }
        }

        // Task 3: Get booking from DB
        public Booking GetBooking(int bookingId)
        {
            if (bookings.ContainsKey(bookingId))
            {
                return bookings[bookingId];
            }
            throw new InvalidBookingIDException("Booking not found.");
        }

        // Task 3: Cancel booking in DB
        public void CancelBooking(int bookingId)
        {
            if (bookings.ContainsKey(bookingId))
            {
                bookings.Remove(bookingId); // Remove from local dictionary
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Bookings WHERE BookingId = @BookingId;";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@BookingId", bookingId);
                        command.ExecuteNonQuery(); // Execute delete command
                    }
                }
            }
            else
            {
                throw new InvalidBookingIDException("Booking not found.");
            }
        }

        // Task 1: Save event to DB

        public void SaveEvent(Event eventObj)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Get the VenueId by saving the venue if it does not exist
                int venueId = SaveVenueIfNotExists(eventObj.Venue);
                Console.WriteLine($"VenueId: {venueId}"); // Log the VenueId

                string query = "INSERT INTO Events (EventName, EventDate, EventTime, VenueId, TotalSeats, TicketPrice, EventType) OUTPUT INSERTED.EventId VALUES (@EventName, @EventDate, @EventTime, @VenueId, @TotalSeats, @TicketPrice, @EventType);";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EventName", eventObj.EventName);
                    command.Parameters.AddWithValue("@EventDate", eventObj.EventDate);
                    command.Parameters.AddWithValue("@EventTime", eventObj.EventTime);
                    command.Parameters.AddWithValue("@VenueId", venueId); // Ensure this points to VenueId
                    command.Parameters.AddWithValue("@TotalSeats", eventObj.TotalSeats);
                    command.Parameters.AddWithValue("@TicketPrice", eventObj.TicketPrice);
                    command.Parameters.AddWithValue("@EventType", eventObj.EventType);

                    // Log all parameters for debugging
                    Console.WriteLine($"Inserting Event: {eventObj.EventName}, VenueId: {venueId}, TotalSeats: {eventObj.TotalSeats}, TicketPrice: {eventObj.TicketPrice}, EventType: {eventObj.EventType}");

                    // Execute the command and set the EventId for event object
                    eventObj.EventId = Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        


        // Task 5: Get event from DB
        public Event GetEvent(int eventId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT e.EventId, e.EventName, e.EventDate, e.EventTime, v.VenueName, v.Address, e.TotalSeats, e.TicketPrice, e.EventType " +
                               "FROM Events e " + // Ensure 'Events' is the correct table name
                               "JOIN Venue v ON e.VenueId = v.VenueId " +
                               "WHERE e.EventId = @EventId;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EventId", eventId);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        Venue venue = new Venue(reader["VenueName"].ToString(), reader["Address"].ToString());

                        string eventType = reader["EventType"].ToString(); // Fetch the event type

                        // Based on the event type, create the appropriate event object
                        switch (eventType)
                        {
                            case "Movie":
                                return new Movie(reader["EventName"].ToString(),
                                                 (DateTime)reader["EventDate"],
                                                 (TimeSpan)reader["EventTime"],
                                                 venue,
                                                 (int)reader["TotalSeats"],
                                                 (decimal)reader["TicketPrice"],
                                                 "Action", // Assuming these fields are valid
                                                 "Actor",
                                                 "Actress");

                            case "Sport":
                                // Assuming you have a Sport class
                                return new Sport(reader["EventName"].ToString(),
                                                 (DateTime)reader["EventDate"],
                                                 (TimeSpan)reader["EventTime"],
                                                 venue,
                                                 (int)reader["TotalSeats"],
                                                 (decimal)reader["TicketPrice"],
                                                 "Team A", // Example field for sports
                                                 "Team B");

                            case "Concert":
                                // Assuming you have a Concert class
                                return new Concert(reader["EventName"].ToString(),
                                                 (DateTime)reader["EventDate"],
                                                 (TimeSpan)reader["EventTime"],
                                                 venue,
                                                 (int)reader["TotalSeats"],
                                                 (decimal)reader["TicketPrice"],
                                                 "Band Name"); // Example field for concerts

                            default:
                                throw new Exception("Unknown event type.");
                        }
                    }
                }
            }
            return null; // Event not found
        }


    }
}*/

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TicketBookingSystem.entity;

namespace TicketBookingSystem
{
    public class BookingSystemRepositoryImpl : IBookingSystemRepository
    {
        private string connectionString = "Data Source=DESKTOP-ULFNUFJ\\SQLEXPRESS;Initial Catalog=TicketBookingSystemNew;Integrated Security=True;";
        private Dictionary<int, Booking> bookings = new Dictionary<int, Booking>();

        public int GetVenueIdByName(string venueName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT VenueId FROM Venue WHERE VenueName = @VenueName";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@VenueName", venueName);
                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : 0;
                }
            }
        }

        public int SaveVenueIfNotExists(Venue venue)
        {
            int venueId = GetVenueIdByName(venue.VenueName);
            if (venueId == 0) // Venue does not exist
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Venue (VenueName, Address) OUTPUT INSERTED.VenueId VALUES (@VenueName, @Address);";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@VenueName", venue.VenueName);
                        command.Parameters.AddWithValue("@Address", venue.Address);
                        venueId = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
            return venueId;
        }

        // Save booking to DB
        public void SaveBooking(Booking booking)
        {
            // Validate the booking object
            if (booking == null || booking.Event == null || booking.Event.EventId <= 0)
            {
                throw new Exception("Invalid booking or event details.");
            }

            // Log for debugging the EventId before saving
            Console.WriteLine($"Saving booking for EventId: {booking.Event.EventId}");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Bookings (EventId, TotalCost, NumberOfTickets) VALUES (@EventId, @TotalCost, @NumberOfTickets); SELECT SCOPE_IDENTITY();";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EventId", booking.Event.EventId);
                    command.Parameters.AddWithValue("@TotalCost", booking.TotalCost);
                    command.Parameters.AddWithValue("@NumberOfTickets", booking.NumberOfTickets);

                    // Log for debugging
                    Console.WriteLine($"Trying to save booking with EventId: {booking.Event.EventId}");

                    // Execute the command and get the booking ID
                    int bookingId = Convert.ToInt32(command.ExecuteScalar());
                    bookings[bookingId] = booking; // Save to local dictionary
                }
            }
        }

        public Booking GetBooking(int bookingId)
        {
            if (bookings.ContainsKey(bookingId))
            {
                return bookings[bookingId];
            }
            throw new InvalidBookingIDException("Booking not found.");
        }

        public void CancelBooking(int bookingId)
        {
            if (bookings.ContainsKey(bookingId))
            {
                bookings.Remove(bookingId);
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Bookings WHERE BookingId = @BookingId;";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@BookingId", bookingId);
                        command.ExecuteNonQuery();
                    }
                }
            }
            else
            {
                throw new InvalidBookingIDException("Booking not found.");
            }
        }

        // Save event to DB
        public void SaveEvent(Event eventObj)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                int venueId = SaveVenueIfNotExists(eventObj.Venue);
                Console.WriteLine($"VenueId: {venueId}");

                string query = "INSERT INTO Events (EventName, EventDate, EventTime, VenueId, TotalSeats, TicketPrice, EventType) OUTPUT INSERTED.EventId VALUES (@EventName, @EventDate, @EventTime, @VenueId, @TotalSeats, @TicketPrice, @EventType);";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EventName", eventObj.EventName);
                    command.Parameters.AddWithValue("@EventDate", eventObj.EventDate);
                    command.Parameters.AddWithValue("@EventTime", eventObj.EventTime);
                    command.Parameters.AddWithValue("@VenueId", venueId);
                    command.Parameters.AddWithValue("@TotalSeats", eventObj.TotalSeats);
                    command.Parameters.AddWithValue("@TicketPrice", eventObj.TicketPrice);
                    command.Parameters.AddWithValue("@EventType", eventObj.EventType);

                    Console.WriteLine($"Inserting Event: {eventObj.EventName}, VenueId: {venueId}, TotalSeats: {eventObj.TotalSeats}, TicketPrice: {eventObj.TicketPrice}, EventType: {eventObj.EventType}");

                    eventObj.EventId = Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        // Get event from DB
        public Event GetEvent(int eventId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT e.EventId, e.EventName, e.EventDate, e.EventTime, v.VenueName, v.Address, e.TotalSeats, e.TicketPrice, e.EventType " +
                               "FROM Events e " +
                               "JOIN Venue v ON e.VenueId = v.VenueId " +
                               "WHERE e.EventId = @EventId;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EventId", eventId);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        Venue venue = new Venue(reader["VenueName"].ToString(), reader["Address"].ToString());
                        string eventType = reader["EventType"].ToString();
                        int eventIdFromDb = (int)reader["EventId"]; // Fetch EventId from the database

                        switch (eventType)
                        {
                            case "Movie":
                                return new Movie(reader["EventName"].ToString(),
                                                 (DateTime)reader["EventDate"],
                                                 (TimeSpan)reader["EventTime"],
                                                 venue,
                                                 (int)reader["TotalSeats"],
                                                 (decimal)reader["TicketPrice"],
                                                 "Action", // Sample data
                                                 "Actor",
                                                 "Actress")
                                {
                                    EventId = eventIdFromDb // Set EventId explicitly
                                };

                            case "Sport":
                                return new Sport(reader["EventName"].ToString(),
                                                 (DateTime)reader["EventDate"],
                                                 (TimeSpan)reader["EventTime"],
                                                 venue,
                                                 (int)reader["TotalSeats"],
                                                 (decimal)reader["TicketPrice"],
                                                 "Team A", // Sample data
                                                 "Team B")
                                {
                                    EventId = eventIdFromDb // Set EventId explicitly
                                };

                            case "Concert":
                                return new Concert(reader["EventName"].ToString(),
                                                 (DateTime)reader["EventDate"],
                                                 (TimeSpan)reader["EventTime"],
                                                 venue,
                                                 (int)reader["TotalSeats"],
                                                 (decimal)reader["TicketPrice"],
                                                 "Band Name") // Sample data
                                {
                                    EventId = eventIdFromDb // Set EventId explicitly
                                };

                            default:
                                throw new Exception("Unknown event type.");
                        }
                    }
                }
            }
            return null; // Event not found
        }

    }
}
