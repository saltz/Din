using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using Models.RadarrMovie;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace Logic
{
    public static class MovieManager
    {
        private static readonly TMDbClient Client = new TMDbClient("33ed603b677a2046b0f3286e83a2253d");

        public static List<SearchMovie> SearchMovie(string searchQuery)
        {
            SearchContainer<SearchMovie> movies = Client.SearchMovieAsync(searchQuery).Result;
            return movies.Results;
        }

        public static string AddMovie(SearchMovie movie, string username)
        {
            List<Image> images = new List<Image>();
            images.Add(new Image(movie.PosterPath));
            var payload = new Movie(movie.Title, images, movie.Id, Convert.ToDateTime(movie.ReleaseDate));
            var result = "";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:7878/api/movie?apikey=814b8903f6394ef49c0665715a8ae2fb");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(payload);
                streamWriter.Write(json);
            }
            try
            {
                var httpResponse = (HttpWebResponse) httpWebRequest.GetResponse();
                ReportEvent(username, movie.Title, "successfull added");
                return httpResponse.StatusDescription;
            }
            catch (WebException)
            {
                ReportEvent(username,movie.Title, "failed");
                return "error";
            }
        }

        public static List<int> GetCurrentMovies()
        {
            List<int> movieIds = new List<int>();
            var webResult = "";

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:7878/api/movie?apikey=814b8903f6394ef49c0665715a8ae2fb");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                webResult = streamReader.ReadToEnd();
            }
            List<Movie> objects = JsonConvert.DeserializeObject<List<Movie>>(webResult);
            foreach (Movie m in objects)
            {
                movieIds.Add(m.tmdbid);
            }
            return movieIds;
        }

        private static void ReportEvent(string user, string movieTitle, string status)
        {
            string path = @"C:\Users\dane\Documents\AddedMovieLogs\" + user + ".txt";
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(movieTitle + " Toegevoegd door: " + user + " datum: " + DateTime.Now + " Status: " +
                                 status);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(movieTitle + " Toegevoegd door: " + user + " datum: " + DateTime.Now + " Status: " +
                                 status);
                }
            }
        }
    }
}
