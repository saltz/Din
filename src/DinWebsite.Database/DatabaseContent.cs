using System;
using System.Collections.Generic;
using System.Data;
using DinWebsite.ExternalModels.AD;
using DinWebsite.ExternalModels.Content;
using DinWebsite.ExternalModels.Exceptions;

namespace DinWebsite.Database
{
    public class DatabaseContent
    {
        public void InsertMovieAddedData(ADObject userAdObject, string movieTitle, string status)
        {
            try
            {
                using (var cmd = Database.Connection.CreateCommand())
                {
                    cmd.CommandText =
                        "INSERT INTO movies (movie_name, person_name, person_accountname, date_added, status)" +
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
                throw new DatabaseException("Error in InserMovieData");
            }
        }

        public List<ContentStatusObject> GetMoviesByAccountname(string accountName)
        {
            var movies = new List<ContentStatusObject>();
            try
            {
                using (var cmd = Database.Connection.CreateCommand())
                {
                    cmd.CommandText =
                        "SELECT movie_name, status, person_accountname, eta, date_added FROM movies WHERE person_accountname = @accountname;";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@accountname", accountName);
                    using (var dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                            if (dataReader["eta"] != DBNull.Value)
                                movies.Add(new ContentStatusObject(Convert.ToString(dataReader["movie_name"]),
                                    Convert.ToString(dataReader["status"]),
                                    Convert.ToString(dataReader["person_accountname"]),
                                    Convert.ToInt32(dataReader["eta"]),
                                    Convert.ToDateTime(dataReader["date_added"])));
                            else
                                movies.Add(new ContentStatusObject(Convert.ToString(dataReader["movie_name"]),
                                    Convert.ToString(dataReader["status"]),
                                    Convert.ToString(dataReader["person_accountname"]),
                                    Convert.ToDateTime(dataReader["date_added"])));
                    }
                }
                return movies;
            }
            catch
            {
                throw new DatabaseException("Error in GetMoviesByAccountname");
            }
        }

        public void UpdateItemStatus(ContentStatusObject item)
        {
            try
            {
                using (var cmd = Database.Connection.CreateCommand())
                {
                    cmd.CommandText =
                        "UPDATE movies SET status = @status WHERE movie_name = @movieName AND person_accountname = @accountName;";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@status", item.Status);
                    cmd.Parameters.AddWithValue("@movieName", item.Title);
                    cmd.Parameters.AddWithValue("@accountName", item.AccountName);
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw new DatabaseException("Error in UpdateItemStatus");
            }
        }

        public void SetItemEta(ContentStatusObject item)
        {
            try
            {
                using (var cmd = Database.Connection.CreateCommand())
                {
                    cmd.CommandText =
                        "UPDATE movies SET eta = @eta WHERE movie_name = @movieName AND person_accountname = @accountName;";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@eta", item.Eta);
                    cmd.Parameters.AddWithValue("@movieName", item.Title);
                    cmd.Parameters.AddWithValue("@accountName", item.AccountName);
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw new DatabaseException("Error in SetItemEta");
            }
        }
    }
}