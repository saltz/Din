using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Din.ExternalModels.Entities;
using Din.ExternalModels.MediaSystem;
using Newtonsoft.Json;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;

namespace Din.Logic.MediaSystem
{
    public class MediaSystem
    {
        private readonly string _url;

        public MediaSystem(string url)
        {
           _url = url;
        }

        public int AddMovie(SearchMovie movie)
        {
            var images = new List<MovieImage> { new MovieImage(movie.PosterPath) };
            var payload = new MediaSystemMovie(movie.Title, images, movie.Id, Convert.ToDateTime(movie.ReleaseDate));
            var httpRequest = new HttpRequestHelper(_url, false);
            return httpRequest.PerformPostRequest(JsonConvert.SerializeObject(payload)).Item1;
        }

        public List<int> GetCurrentMovies()
        {
            var movieIds = new List<int>();
            var httpRequest = new HttpRequestHelper(_url, false);
            var objects = JsonConvert.DeserializeObject<List<MediaSystemMovie>>(httpRequest.PerformGetRequest());
            foreach (var m in objects)
                movieIds.Add(m.Tmdbid);
            return movieIds;
        }

        private List<AddedContent> CheckIfItemIsCompleted(List<AddedContent> items)
        {
            //TODO SORT OUT THIS CODE
            var httpRequest = new HttpRequestHelper(_url, false);  
            var objects = JsonConvert.DeserializeObject<List<MediaSystemMovie>>(httpRequest.PerformGetRequest());
            foreach (var i in items)
            foreach (var m in objects)
            {
                if (!i.Title.Equals(m.Title)) continue;
                if (!m.Downloaded) continue;
                i.Status = ContentStatus.Downloaded;
                // databaseContent.UpdateItemStatus(i);
                break;
            }
            return items;
        }
    }
}
