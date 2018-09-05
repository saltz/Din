using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Din.ExternalModels.MediaSystem;
using Din.ExternalModels.Utils;
using Din.Service.Clients.Interfaces;
using Newtonsoft.Json;
using TMDbLib.Objects.Search;

namespace Din.Service.Clients.Concrete
{

    //TODO rename to client and supply HttpClientFactory
    public class MediaSystemClient : IMediaSystemClient
    {
<<<<<<< HEAD:src/Din.Service/DeprecatedObjects/MediaSystemDEPRECATED.cs
        private readonly string _url;
        private readonly string _key;
        private readonly string _savePath;

        /* TODO These are all DEPRECATED and should be removed
=======
>>>>>>> feature/account-view:src/Din.Service/Systems/MediaSystem.cs
        private readonly string _movieSystemUrl = MainService.PropertyFile.Get("movieSystemUrl");
        private readonly string _movieSystemKey = MainService.PropertyFile.Get("movieSystemKey");
        private readonly string _tvShowSystemUrl = MainService.PropertyFile.Get("tvShowSystemUrl");
        private readonly string _tvShowSystemKey = MainService.PropertyFile.Get("tvShowSystemKey");
<<<<<<< HEAD:src/Din.Service/DeprecatedObjects/MediaSystemDEPRECATED.cs
        */
        public MediaSystemClient(string url, string key, string savePath)
        {
            _url = url;
            _key = key;
            _savePath = savePath;
        }
=======
>>>>>>> feature/account-view:src/Din.Service/Systems/MediaSystem.cs

        public async Task<List<int>> GetCurrentMoviesAsync()
        {
            var movieIds = new List<int>();
            var objects =
                JsonConvert.DeserializeObject<List<MediaSystemMovie>>(
<<<<<<< HEAD:src/Din.Service/DeprecatedObjects/MediaSystemDEPRECATED.cs
                    await new HttpRequestHelper(_url, false).PerformGetRequestAsync());
=======
                    await new HttpRequestHelper(BuildRequestUrl(Selection.MovieSystem, Endpoint.Movie), false)
                        .PerformGetRequestAsync());
>>>>>>> feature/account-view:src/Din.Service/Systems/MediaSystem.cs
            foreach (var m in objects)
                movieIds.Add(m.Tmdbid);
            return movieIds;
        }

        public async Task<List<string>> GetCurrentTvShowsAsync()
        {
            var tvShowIds = new List<string>();
            var objects =
                JsonConvert.DeserializeObject<List<MediaSystemTvShow>>(
<<<<<<< HEAD:src/Din.Service/DeprecatedObjects/MediaSystemDEPRECATED.cs
                    await new HttpRequestHelper(_url, false).PerformGetRequestAsync());
=======
                    await new HttpRequestHelper(BuildRequestUrl(Selection.TvShowSystem, Endpoint.Series), false)
                        .PerformGetRequestAsync());
>>>>>>> feature/account-view:src/Din.Service/Systems/MediaSystem.cs
            foreach (var t in objects)
                tvShowIds.Add(t.Title.ToLower());
            return tvShowIds;
        }

        public async Task<int> AddMovieAsync(SearchMovie movie)
        {
            var images = new List<MediaSystemImage> {new MediaSystemImage("poster", movie.PosterPath)};
            var payload = new MediaSystemMovie(movie.Title, Convert.ToDateTime(movie.ReleaseDate), movie.Id, images,
<<<<<<< HEAD:src/Din.Service/DeprecatedObjects/MediaSystemDEPRECATED.cs
                _savePath);
=======
                MainService.PropertyFile.Get("movieSystemFileLocation"));
>>>>>>> feature/account-view:src/Din.Service/Systems/MediaSystem.cs
            var response =
                await new HttpRequestHelper(BuildRequestUrl(Selection.MovieSystem, Endpoint.Movie), false)
                    .PerformPostRequestAsync(
                        JsonConvert.SerializeObject(payload));
            return response.Item1;
        }

        public async Task<int> AddTvShowAsync(SearchTv show, string tvdbId, IEnumerable<SearchTvSeason> seasons)
        {
            var images = new List<MediaSystemImage> {new MediaSystemImage("poster", show.PosterPath)};
            var payload = new MediaSystemTvShow(show.Name, Convert.ToDateTime(show.FirstAirDate), tvdbId, images,
                seasons,
<<<<<<< HEAD:src/Din.Service/DeprecatedObjects/MediaSystemDEPRECATED.cs
               _savePath);
            var response =
                await new HttpRequestHelper(_url, false).PerformPostRequestAsync(
                    JsonConvert.SerializeObject(payload));
=======
                MainService.PropertyFile.Get("tvShowSystemFileLocation"));
            var response =
                await new HttpRequestHelper(
                        BuildRequestUrl(Selection.TvShowSystem, Endpoint.Series), false)
                    .PerformPostRequestAsync(
                        JsonConvert.SerializeObject(payload));
>>>>>>> feature/account-view:src/Din.Service/Systems/MediaSystem.cs
            return response.Item1;
        }

        public async Task<List<MediaSystemMovieCalendarObject>> GetMovieCalendarAsync()
        {
            return JsonConvert.DeserializeObject<List<MediaSystemMovieCalendarObject>>(
                await new HttpRequestHelper(BuildRequestUrl(Selection.MovieSystem, Endpoint.Calendar) + "&start=" +
                                            DateTime.Now.ToString("yyyy-MM-dd") + "&end=" +
                                            DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd"), false)
                    .PerformGetRequestAsync());
        }

        public async Task<List<MediaSystemTvShowCalendarObject>> GetTvShowCalendarAsync()
        {
            return JsonConvert.DeserializeObject<List<MediaSystemTvShowCalendarObject>>(
                await new HttpRequestHelper(BuildRequestUrl(Selection.TvShowSystem, Endpoint.Calendar) + "&start=" +
                                            DateTime.Now.ToString("yyyy-MM-dd") + "&end=" + DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd"), false)
                    .PerformGetRequestAsync());
        }

        //TODO Check this code for optimization
        public async Task<List<AddedContent>> CheckIfItemIsCompletedAsync(List<AddedContent> content)
        {
            var httpRequest = new HttpRequestHelper(_url, false);
            var current =
                JsonConvert.DeserializeObject<List<MediaSystemMovie>>(await httpRequest.PerformGetRequestAsync());
            foreach (var i in content)
            foreach (var m in current)
            {
                if (!i.Title.Equals(m.Title)) continue;
                if (!m.Downloaded) continue;
                i.Status = ContentStatus.Done;
                break;
            }

            return content;
        }

        private string BuildRequestUrl(Selection system, Endpoint endpoint)
        {
            return system.Equals(Selection.MovieSystem)
<<<<<<< HEAD:src/Din.Service/DeprecatedObjects/MediaSystemDEPRECATED.cs
                ? new StringBuilder().Append(_url).Append(endpoint.ToString().ToLower())
                    .Append(_url).ToString()
                : new StringBuilder().Append(_url).Append(endpoint.ToString().ToLower())
                    .Append(_url).ToString();
=======
                ? new StringBuilder().Append(_movieSystemUrl).Append(endpoint.ToString().ToLower())
                    .Append(_movieSystemKey).ToString()
                : new StringBuilder().Append(_tvShowSystemUrl).Append(endpoint.ToString().ToLower())
                    .Append(_tvShowSystemKey).ToString();
>>>>>>> feature/account-view:src/Din.Service/Systems/MediaSystem.cs
        }
    }

    internal enum Selection
    {
        MovieSystem,
        TvShowSystem
    }

    internal enum Endpoint
    {
        Movie,
        Series,
        Calendar
    }
}
