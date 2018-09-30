using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Din.Service.Clients.Abstractions;
using Din.Service.Clients.Interfaces;
using Din.Service.Clients.RequestObjects;
using Din.Service.Clients.ResponseObjects;
using Din.Service.Config.Interfaces;
using Newtonsoft.Json;

namespace Din.Service.Clients.Concrete
{
    public class TvShowClient : BaseClient, ITvShowClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITvShowClientConfig _config;

        public TvShowClient(IHttpClientFactory httpClientFactory, ITvShowClientConfig config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        public async Task<IEnumerable<TcTvShowResponse>> GetCurrentTvShowsAsync()
        {
            var client = _httpClientFactory.CreateClient();

            return JsonConvert.DeserializeObject<IEnumerable<TcTvShowResponse>>(
                await client.GetStringAsync(BuildUrl(_config.Url, "series", $"?apikey={_config.Key}")));
        }

        public async Task<TcTvShowResponse> GetTvShowByIdAsync(int id)
        {
            var client = _httpClientFactory.CreateClient();

            return JsonConvert.DeserializeObject<TcTvShowResponse>(
                await client.GetStringAsync(BuildUrl(_config.Url, $"series/{id}", $"?apikey={_config.Key}")));
        }

        public async Task<(bool status, int systemId)> AddTvShowAsync(TcRequest tvShow)
        {
            tvShow.RootFolderPath = _config.SaveLocation;
            var client = _httpClientFactory.CreateClient();

            var response = await client.PostAsync(BuildUrl(_config.Url, "series", $"?apikey={_config.Key}"),
                new StringContent(JsonConvert.SerializeObject(tvShow)));

            return (response.StatusCode.Equals(HttpStatusCode.Created),
                JsonConvert.DeserializeObject<TcTvShowResponse>(await response.Content.ReadAsStringAsync()).SystemId);
        }

        public async Task<IEnumerable<TcCalendarResponse>> GetCalendarAsync()
        {
            var client = _httpClientFactory.CreateClient();

            return JsonConvert.DeserializeObject<IEnumerable<TcCalendarResponse>>(
                await client.GetStringAsync(BuildUrl(_config.Url, "calendar", $"?apikey={_config.Key}",
                    GetTimespan())));
        }

        private string GetTimespan()
        {
            return $"&start={DateTime.Now.Date.AddDays(-14):MM-dd-yyyy}&end={DateTime.Now.AddMonths(1):MM-dd-yyyy}";
        }
    }
}