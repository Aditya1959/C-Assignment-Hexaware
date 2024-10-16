using System;
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
