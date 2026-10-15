using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIUBSportsEventManager
{
    public static class Database
    {
        // connection string

        private static readonly string connectionString =
            @"Data Source=DESKTOP-U9K8HEV\SQLEXPRESS;Initial Catalog=AIUBEventsDB;Integrated Security=True;";


        // Method to get the database connection
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
