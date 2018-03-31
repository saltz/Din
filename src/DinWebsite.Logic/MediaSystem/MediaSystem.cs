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
        private readonly string _url;

        public MediaSystem(string url)
        {
           _url = url;
        }

        public string AddMovie(SearchMovie movie)
        {
            var images = new List<MovieImage> { new MovieImage(movie.PosterPath) };
            var payload = new MediaSystemMovie(movie.Title, images, movie.Id, Convert.ToDateTime(movie.ReleaseDate));
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(_url);
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

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(_url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream() ?? throw new InvalidOperationException()))
            {
                webResult = streamReader.ReadToEnd();
            }
            var objects = JsonConvert.DeserializeObject<List<MediaSystemMovie>>(webResult);
            foreach (var m in objects)
                movieIds.Add(m.Tmdbid);
            return movieIds;
        }
    }
}
