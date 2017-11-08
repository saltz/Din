using System;
using System.Data;
using Models.AD;
using MySql.Data.MySqlClient;

namespace Database
{
    public class ContentMovie
    {
        public void InsertMovieAddedData(AdObject userAdObject, string movieTitle, string status)
        {
            try
            {
                using (MySqlCommand cmd = Database.Connection.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO movies (movie_name, person_name, person_accountname, date_added, status)" +
                                      " VALUES (@movie_name, @person_name, @person_accountname, @date_added, @status);";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@movie_name", movieTitle);
                    cmd.Parameters.AddWithValue("@person_name", userAdObject.Name);
                    cmd.Parameters.AddWithValue("@person_accountname", userAdObject.SAMAccountName);
                    cmd.Parameters.AddWithValue("@date_added", DateTime.Now);
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                Database.Connection.Close();
            }
        }
    }
}
