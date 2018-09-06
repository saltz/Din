using System.Net.Http;
using System.Threading.Tasks;
using Din.Service.Clients.Interfaces;
using Din.Service.Config.Interfaces;
using Din.Service.DTO.Context;
using Newtonsoft.Json;

namespace Din.Service.Clients.Concrete
{
    public class IpStackClient : BaseClient, IIpStackClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IIpStackClientConfig _config;

        public IpStackClient(IHttpClientFactory httpClientFactory, IIpStackClientConfig config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        public async Task<LoginLocationDTO> GetLocation(string ip)
        {
            var client = _httpClientFactory.CreateClient();
            return JsonConvert.DeserializeObject<LoginLocationDTO>(await client.GetStringAsync(BuildUrl(new[] {ip})));
        }

        protected override string BuildUrl(string[] parameters)
        {
            return $"{_config.Url}{parameters[0]}?access_key={_config.Key}";
        }
    }
}