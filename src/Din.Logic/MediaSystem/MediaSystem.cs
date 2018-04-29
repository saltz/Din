using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
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

        public async Task<int> AddMovieAsync(SearchMovie movie)
        {
            var images = new List<MovieImage> { new MovieImage(movie.PosterPath) };
            var payload = new MediaSystemMovie(movie.Title, images, movie.Id, Convert.ToDateTime(movie.ReleaseDate));
            var httpRequest = new HttpRequestHelper(_url, false);
            var response = await httpRequest.PerformPostRequestAsync(JsonConvert.SerializeObject(payload));
            return response.Item1;
        }

        public async Task<List<int>> GetCurrentMoviesAsync()
        {
            var movieIds = new List<int>();
            var httpRequest = new HttpRequestHelper(_url, false);
            var objects = JsonConvert.DeserializeObject<List<MediaSystemMovie>>(await httpRequest.PerformGetRequestAsync());
            foreach (var m in objects)
                movieIds.Add(m.Tmdbid);
            return movieIds;
        }

        public async Task<List<AddedContent>> CheckIfItemIsCompletedAsync(List<AddedContent> content)
        {
            var httpRequest = new HttpRequestHelper(_url, false);  
            var current = JsonConvert.DeserializeObject<List<MediaSystemMovie>>(await httpRequest.PerformGetRequestAsync());
            foreach (var i in content)
            foreach (var m in current)
            {
                if (!i.Title.Equals(m.Title)) continue;
                if (!m.Downloaded) continue;
                i.Status = ContentStatus.Downloaded;
                break;
            }
            return content;
        }
    }
}
