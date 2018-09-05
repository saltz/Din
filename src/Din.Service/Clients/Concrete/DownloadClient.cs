using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Din.ExternalModels.DownloadClient;
using Din.ExternalModels.Utils;
using Din.Service.Clients.Interfaces;
using Newtonsoft.Json;

namespace Din.Service.Clients.Concrete
{
    public class DownloadClient : IDownloadClient
    {
        private HttpRequestHelper _httpRequest;

        public DownloadClient(string url, string pwd)
        {
            Authenticate(url, pwd).Wait();
        }

        private async Task Authenticate(string url, string pwd)
        {
            var payload = new DcSingleParam("auth.login", new List<string> {pwd}, 1);
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
        }

        public async Task<ICollection<DownloadClientItem>> GetAllItemsAsync()
        {
            var payload = new DcSingleParam("webapi.get_torrents", new List<string>(), 1);
            try
            {
                var response = JsonConvert.DeserializeObject<DcResponseObject>((await _httpRequest.PerformPostRequestAsync(JsonConvert.SerializeObject(payload))).Item2);
                return new List<DownloadClientItem>(response.Result.Items);
            }
            catch
            {
                throw new DownloadClientException("Failed to get all items");
            }
        }

        public async Task<DownloadClientItem> GetItemStatusAsync(string itemHash)
        {
            var payload = new DcSCollectionParam("webapi.get_torrents", new List<List<string>>
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
            }, 1);
            try
            {
                return JsonConvert.DeserializeObject<DcResponseObject>((await _httpRequest.PerformPostRequestAsync(JsonConvert.SerializeObject(payload))).Item2).Result.Items[0];
            }
            catch
            {
                throw new DownloadClientException("Failed to get item status");
            }
        }
    }
}