using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Din.ExternalModels.Entities;
using Din.ExternalModels.MediaSystem;
using Din.ExternalModels.Utils;
using Din.Service.Classes;
using Newtonsoft.Json;
using TMDbLib.Objects.Search;

namespace Din.Service.Systems
{
    public class MediaSystem
    {
        private static readonly string _url = MainService.PropertyFile.get("mediaSystem");

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
