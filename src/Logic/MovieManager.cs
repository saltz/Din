using System;
using System.Collections.Generic;
using System.IO;
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
        private static readonly TMDbClient Client = new TMDbClient("33ed603b677a2046b0f3286e83a2253d");

        public static List<SearchMovie> SearchMovie(string searchQuery)
        {
            SearchContainer<SearchMovie> movies = Client.SearchMovieAsync(searchQuery).Result;
            return movies.Results;
        }

        public static string AddMovie(SearchMovie movie, AdObject userAdObject)
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

        private static void ReportEvent(AdObject userAdObject, string movieTitle, string status)
        {
            ContentMovie contentMovie = new ContentMovie();
            contentMovie.InsertMovieAddedData(userAdObject, movieTitle, status);
        }
    }
}
