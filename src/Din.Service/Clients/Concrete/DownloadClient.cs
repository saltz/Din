using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Din.Service.Clients.Interfaces;
using Din.Service.Clients.RequestObjects;
using Din.Service.Clients.ResponseObjects;
using Din.Service.Config.Interfaces;
using Newtonsoft.Json;

namespace Din.Service.Clients.Concrete
{
    public class DownloadClient : BaseClient, IDownloadClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IDownloadClientConfig _config;

        public DownloadClient(IHttpClientFactory httpClientFactory, IDownloadClientConfig config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
            Authenticate().Wait(); //TODO check singleton
        }

        private async Task Authenticate()
        {
            var payload = new DcSingleParamRequest
            {
                Id = 1,
                Method = "auth.login",
                Params = new List<string> {_config.Password}
            };

            //TODO Check if the cookiecontainer and decopression methods are needed
            var client = _httpClientFactory.CreateClient();

            await client.PostAsync(_config.Url, new StringContent(JsonConvert.SerializeObject(payload)));

            /* OLD CODE NEEDED FOR TODO
            _httpRequest = new HttpRequestHelper(url, true);
            _httpRequest.SetDecompressionMethods(new List<DecompressionMethods>(){DecompressionMethods.Deflate, DecompressionMethods.GZip});        
            try
            {
                await _httpRequest.PerformPostRequestAsync(JsonConvert.SerializeObject(payload));
            }
            catch
            {
                throw new DownloadClientException("Error occured while authenticating");
            }
            */
        }

        public async Task<DcResponse> GetAllItemsAsync()
        {
            var payload = new DcSingleParamRequest
            {
                Id = 1,
                Method = "webapi.get_torrents",
                Params = new List<string>()
            };

            var client = _httpClientFactory.CreateClient();

            //TODO
            var response = await client.PostAsync(_config.Url, new StringContent(JsonConvert.SerializeObject(payload)));
            return new DcResponse();
            /* OLD CODE
            try
            {
                var response = JsonConvert.DeserializeObject<DcResponseObject>((await _httpRequest.PerformPostRequestAsync(JsonConvert.SerializeObject(payload))).Item2);
                return new List<DownloadClientItem>(response.Result.Items);
            }
            catch
            {
                throw new DownloadClientException("Failed to get all items");
            }
            */
        }

        public async Task<DcResponseItem> GetItemStatusAsync(string itemHash)
        {
            var payload = new DcParamCollectionRequest
            {
                Id = 1,
                Method = "webapi.get_torrents",
                Params = new List<ICollection<string>>
                {
                    new List<string>
                    {
                        itemHash
                    },
                    new List<string>
                    {
                        "eta",
                        "files",
                        "file_progress"
                    }
                }
            };

            var client = _httpClientFactory.CreateClient();
            //TODO
            var response = await client.PostAsync(_config.Url, new StringContent(JsonConvert.SerializeObject(payload)));
            return new DcResponseItem();

            /* OLD CODE
            try
            {
                return JsonConvert.DeserializeObject<DcResponseObject>((await _httpRequest.PerformPostRequestAsync(JsonConvert.SerializeObject(payload))).Item2).Result.Items[0];
            }
            catch
            {
                throw new DownloadClientException("Failed to get item status");
            }
            */
        }
    }
}