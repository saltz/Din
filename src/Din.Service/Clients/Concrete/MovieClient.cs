using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

            var response = JsonConvert.DeserializeObject<List<MCMovieResponse>>(await client.GetStringAsync(BuildUrl(new[] {_config.Url, "movie", _config.Key})));

            return response.Select(r => r.Id).AsEnumerable();
        }

        public async Task<bool> AddMovieAsync(MCRequest movie)
        {
            movie.RootFolderPath = _config.SaveLocation;
            var client = _httpClientFactory.CreateClient();

            var response = await client.PostAsync(BuildUrl(new[] {_config.Url, "movie", _config.Key}),
                new StringContent(JsonConvert.SerializeObject(movie)));

            return response.StatusCode.Equals(HttpStatusCode.Created);
        }

        protected override string BuildUrl(params string[] parameters)
        {
            return $"{parameters[0]}{parameters[1]}?apikey={parameters[2]}";
        }
    }
}
