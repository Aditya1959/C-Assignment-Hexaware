<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
//@"Server=DESKTOP-ULFNUFJ\SQLEXPRESS;Database=TicketBookingSystem;Integrated Security=True;"
using System.Data.SqlClient;

namespace TicketBookingSystem
{
    public static class DBUtil
    {
        public static SqlConnection GetConnection(string connectionString)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBookingSystem.util
{
    public static class DBUtil
    {
        public static SqlConnection GetDBConn()
        {
            // Replace with your actual connection string
            string connectionString = "your_connection_string_here";
            return new SqlConnection(connectionString);
        }
    }
}
>>>>>>> c11cbd52b6042793bd6f0394833f32c967ff05fa
