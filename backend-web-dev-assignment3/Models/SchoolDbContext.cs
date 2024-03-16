using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backend_web_dev_assignment3.Models
{
    public class SchoolDbContext
    {
        private static string User { get { return "root"; } }
        private static string Password { get { return "root"; } }
        private static string Database { get { return "school_database"; } }
        private static string Server { get { return "localhost"; } }
        private static string Port { get { return "3306"; } }

        protected static string ConnectionString {
            get {
                return "server = " + Server
                     + "; user = " + User
                     + "; database = " + Database
                     + "; port = " + Port
                     + "; password = " + Password
                     + "; convert zero datetime = True";
            }
        }

        /// <summary>
        /// Method to connect to the school_database
        /// </summary>
        /// <returns>Returns a MySqlConnection Object</returns>
        public MySqlConnection AccessDatabase() {
            return new MySqlConnection(ConnectionString);
        }
    }
}