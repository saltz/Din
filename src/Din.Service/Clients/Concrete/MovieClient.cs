using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Din.Service.Clients.Interfaces;
using Din.Service.Clients.RequestObjects;
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

            var response = await client.GetAsync(BuildUrl(new[] {_config.Url, "movie", _config.Key}));

            //TODO Filter trough response collection LINQ get all movieId
            return new List<int>();
        }

        public async Task<bool> AddMovieAsync(MCRequest movie)
        {
            movie.RootFolderPath = _config.SaveLocation;
            var client = _httpClientFactory.CreateClient();

            var response = await client.PostAsync(BuildUrl(new[] {_config.Url, "movie", _config.Key}),
                new StringContent(JsonConvert.SerializeObject(movie)));
            //TODO
            return response.StatusCode.Equals(HttpStatusCode.Created);
        }

        protected override string BuildUrl(string[] parameters)
        {
            return $"{parameters[0]}{parameters[1]}?apikey={parameters[2]}";
        }
    }
}
