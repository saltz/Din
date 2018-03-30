using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using DinWebsite.ExternalModels.Movie;
using Newtonsoft.Json;
using TMDbLib.Objects.Search;

namespace DinWebsite.Logic.MediaSystem
{
    public class MediaSystem
    {
        private readonly string requestUri;

        public MediaSystem()
        {
           requestUri = File.ReadLines("C:/din_properties/api_movie").First();
        }

        public string AddMovie(SearchMovie movie)
        {
            var images = new List<Image> { new Image(movie.PosterPath) };
            var payload = new Movie(movie.Title, images, movie.Id, Convert.ToDateTime(movie.ReleaseDate));
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUri);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                var json = JsonConvert.SerializeObject(payload);
                streamWriter.Write(json);
            }
            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                // ReportEvent(userAdObject, movie.Title, "added");
                return httpResponse.StatusDescription;
            }
            catch (WebException)
            {
                //  ReportEvent(userAdObject, movie.Title, "failed to add");
                return "error";
            }
        }

        public List<int> GetCurrentMovies()
        {
            var movieIds = new List<int>();
            string webResult;

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUri);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream() ?? throw new InvalidOperationException()))
            {
                webResult = streamReader.ReadToEnd();
            }
            var objects = JsonConvert.DeserializeObject<List<Movie>>(webResult);
            foreach (var m in objects)
                movieIds.Add(m.Tmdbid);
            return movieIds;
        }
    }
}
