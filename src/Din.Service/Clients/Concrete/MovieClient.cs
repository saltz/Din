using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Din.Service.Clients.Interfaces;
using Din.Service.Clients.RequestObjects;
using Din.Service.Clients.ResponseObjects;
using Din.Service.Config.Interfaces;
using Newtonsoft.Json;

namespace Din.Service.Clients.Concrete
{
    public class MovieClient : BaseClient, IMovieClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMovieClientConfig _config;

        public MovieClient(IHttpClientFactory httpClientFactory, IMovieClientConfig config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        public async Task<IEnumerable<int>> GetCurrentMoviesAsync()
        {
            var client = _httpClientFactory.CreateClient();

            var response = JsonConvert.DeserializeObject<List<MCMovieResponse>>(await client.GetStringAsync(BuildUrl(_config.Url, "movie", $"?apikey={_config.Key}")));

            return response.Select(r => r.Id).AsEnumerable();
        }

        public async Task<bool> AddMovieAsync(MCRequest movie)
        {
            movie.RootFolderPath = _config.SaveLocation;
            var client = _httpClientFactory.CreateClient();

            var response = await client.PostAsync(BuildUrl(_config.Url, "movie", $"?apikey={_config.Key}"),
                new StringContent(JsonConvert.SerializeObject(movie)));

            return response.StatusCode.Equals(HttpStatusCode.Created);
        }

        public async Task<MCCalendarResponse> GetCalendar()
        {
            var client = _httpClientFactory.CreateClient();

            return JsonConvert.DeserializeObject<MCCalendarResponse>(
                    await client.GetStringAsync(BuildUrl(_config.Url, "calendar", $"?apikey={_config.Key}", $"&start={DateTime.Now}&end={DateTime.Now.AddMonths(1)}")));
        }
    }
}
