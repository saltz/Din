using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Din.ExternalModels.Entities;
using Din.ExternalModels.MediaSystem;
using Din.ExternalModels.TvtbClient;
using Din.ExternalModels.Utils;
using Din.Service.Classes;
using Newtonsoft.Json;
using TMDbLib.Objects.Search;

namespace Din.Service.Systems
{
    public class MediaSystem
    {
        private readonly string _movieSystemUrl = MainService.PropertyFile.get("movieSystem");
        private readonly string _tvShowSystemUrl = MainService.PropertyFile.get("tvShowSystem");

        public async Task<List<int>> GetCurrentMoviesAsync()
        {
            var movieIds = new List<int>();
            var objects =
                JsonConvert.DeserializeObject<List<MediaSystemMovie>>(
                    await new HttpRequestHelper(_movieSystemUrl, false).PerformGetRequestAsync());
            foreach (var m in objects)
                movieIds.Add(m.Tmdbid);
            return movieIds;
        }

        public async Task<List<int>> GetCurrentTvShowsAsync()
        {
            var tvShowIds = new List<int>();
            var objects =
                JsonConvert.DeserializeObject<List<MediaSystemTvShow>>(
                    await new HttpRequestHelper(_tvShowSystemUrl, false).PerformGetRequestAsync());
            foreach (var t in objects)
                tvShowIds.Add(t.TvShowId);
            return tvShowIds;
        }

        public async Task<int> AddMovieAsync(SearchMovie movie)
        {
            var images = new List<MediaSystemImage> {new MediaSystemImage("poster", movie.PosterPath)};
            var payload = new MediaSystemMovie(movie.Title, images, movie.Id, Convert.ToDateTime(movie.ReleaseDate),
                MainService.PropertyFile.get("movieSystemFileLocation"));
            var response =
                await new HttpRequestHelper(_movieSystemUrl, false).PerformPostRequestAsync(
                    JsonConvert.SerializeObject(payload));
            return response.Item1;
        }

        public async Task<int> AddTvShowAsync(TvdbShow show, List<TvShowSeason> seasons)
        {
            var images = new List<MediaSystemImage> {new MediaSystemImage("banner", show.Banner)};
            var date = show.FirstAired ?? DateTime.MinValue;
            var payload = new MediaSystemTvShow(show.Title, date, show.ShowId, images, seasons,
                MainService.PropertyFile.get("tvShowSystemFileLocation"));
            var response =
                await new HttpRequestHelper(_tvShowSystemUrl, false).PerformPostRequestAsync(
                    JsonConvert.SerializeObject(payload));
            return response.Item1;
        }

        public async Task<List<AddedContent>> CheckIfItemIsCompletedAsync(List<AddedContent> content)
        {
            var httpRequest = new HttpRequestHelper(_movieSystemUrl, false);
            var current =
                JsonConvert.DeserializeObject<List<MediaSystemMovie>>(await httpRequest.PerformGetRequestAsync());
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