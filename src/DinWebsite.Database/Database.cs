using System.IO;
using System.Linq;
using MySql.Data.MySqlClient;

namespace DinWebsite.Database
{
    public static class Database
    {
        private static readonly string ConnectionString = File.ReadLines("C:/din_properties/dblogin").First();

        public static MySqlConnection Connection
        {
            get
            {
                var connection = new MySqlConnection(ConnectionString);
                connection.Open();
                return connection;
            }
        }
    }
}