using System.Net.Http;
using System.Threading.Tasks;
using Din.Service.Clients.Interfaces;
using Din.Service.Clients.ResponseObjects;
using Din.Service.Config.Interfaces;
using Newtonsoft.Json;

namespace Din.Service.Clients.Concrete
{
    public class GiphyClient : BaseClient, IGiphyClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IGiphyClientConfig _config;

        public GiphyClient(IHttpClientFactory httpClientFactory, IGiphyClientConfig config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        public async Task<GiphyResponse> GetRandomGifAsync(GiphyTag tag)
        {
            var client = _httpClientFactory.CreateClient();

            return JsonConvert.DeserializeObject<GiphyResponse>(await client
                .GetStringAsync(BuildUrl(_config.Url, _config.Key, tag.ToString().ToLower())));
        }

        protected override string BuildUrl(params string[] p)
        {
            return $"{p[0]}?api_key={p[1]}&tag={p[2]}&rating=G";
        }
    }

    public enum GiphyTag
    {
        Nicetry,
        Bye,
        Funny,
        Trending,
        Error
    }
}