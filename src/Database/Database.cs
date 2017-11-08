using System.Data.SqlClient;
using System.IO;
using System.Linq;
using MySql.Data.MySqlClient;

namespace Database
{
    public static class Database
    {
        private static readonly string ConnectionString = File.ReadLines("C:/Users/dane/documents/dblogin").First();

        public static MySqlConnection Connection
        {
            get
            {
                MySqlConnection connection = new MySqlConnection(ConnectionString);
                connection.Open();
                return connection;
            }
        }
    }
}
