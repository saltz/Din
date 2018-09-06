using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Din.Service.Clients.Interfaces;
using Din.Service.Clients.ResponseObjects;
using Din.Service.Config.Interfaces;
using Newtonsoft.Json;

namespace Din.Service.Clients.Concrete
{
    public class UnsplashClient : BaseClient, IUnsplashClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IUnsplashClientConfig _config;

        public UnsplashClient(IHttpClientFactory httpClientFactory, IUnsplashClientConfig config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        public async Task<ICollection<UnsplashResponseObject>> GetBackgroundCollection()
        {
            var client = _httpClientFactory.CreateClient();

            return new List<UnsplashResponseObject>(JsonConvert.DeserializeObject<ICollection<UnsplashResponseObject>>(
                await client.GetStringAsync(BuildUrl(new[] {_config.Url, _config.Key}))));
        }

        protected override string BuildUrl(string[] parameters)
        {
            return $"{parameters[0]}?client_id={parameters[1]}&orientation=landscape&count=20&featured";
        }
    }
}