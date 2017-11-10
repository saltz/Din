using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Database;
using Models.AD;
using Models.RadarrMovie;
using Newtonsoft.Json;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace Logic
{
    public static class MovieManager
    {
        private static readonly TMDbClient Client = new TMDbClient(File.ReadLines("C:/din_properties/api_key").First());
        private static readonly string ApiMovieRequest = File.ReadLines("C:/din_properties/api_movie").First();

        public static List<SearchMovie> SearchMovie(string searchQuery)
        {
            SearchContainer<SearchMovie> movies = Client.SearchMovieAsync(searchQuery).Result;
            return movies.Results;
        }

        public static string AddMovie(SearchMovie movie, ADObject userAdObject)
        {
            List<Image> images = new List<Image>();
            images.Add(new Image(movie.PosterPath));
            var payload = new Movie(movie.Title, images, movie.Id, Convert.ToDateTime(movie.ReleaseDate));
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(ApiMovieRequest);
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
                ReportEvent(userAdObject, movie.Title, "added");
                return httpResponse.StatusDescription;
            }
            catch (WebException)
            {
                ReportEvent(userAdObject,movie.Title, "failed to add");
                return "error";
            }
        }

        public static List<int> GetCurrentMovies()
        {
            List<int> movieIds = new List<int>();
            string webResult;

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(ApiMovieRequest);
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

        private static void ReportEvent(ADObject userAdObject, string movieTitle, string status)
        {
            DatabaseContent databaseContent = new DatabaseContent();
            databaseContent.InsertMovieAddedData(userAdObject, movieTitle, status);
        }
    }
}
