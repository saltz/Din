using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Din.ExternalModels.Exceptions;
using Din.ExternalModels.Utils;
using Newtonsoft.Json;

namespace Din.ExternalModels.DownloadClient
{
    public class DownloadClient
    {
        private HttpRequestHelper _httpRequest;
        private readonly string _url;

        public DownloadClient(string url, string pwd)
        {
            _url = url;
            Authenticate(pwd);
        }

        private void Authenticate(string pwd)
        {
            var payload = new DownloadClientRequestObject1("auth.login", new List<string> {pwd}, 1);
            _httpRequest = new HttpRequestHelper(_url, true);
            _httpRequest.SetDecompressionMethods(new List<DecompressionMethods>(){DecompressionMethods.Deflate, DecompressionMethods.GZip});        
            try
            {
                var response = _httpRequest.PerformPostRequestAsync(JsonConvert.SerializeObject(payload));
            }
            catch
            {
                throw new DownloadClientException("Error occured while authenticating");
            }
        }

        public async Task<List<DownloadClientItem>> GetAllItemsAsync()
        {
            var payload = new DownloadClientRequestObject1("webapi.get_torrents", new List<string>(), 1);
            try
            {
                var response = JsonConvert.DeserializeObject<DownloadClientResponseObject>((await _httpRequest.PerformPostRequestAsync(JsonConvert.SerializeObject(payload))).Item2);
                return new List<DownloadClientItem>(response.Result.Items);
            }
            catch
            {
                throw new DownloadClientException("Failed to get all items");
            }
        }

        public async Task<DownloadClientItem> GetItemStatusAsync(string itemHash)
        {
            var payload = new DownloadClientRequestObject2("webapi.get_torrents", new List<List<string>>
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
                return JsonConvert.DeserializeObject<DownloadClientResponseObject>((await _httpRequest.PerformPostRequestAsync(JsonConvert.SerializeObject(payload))).Item2).Result.Items[0];
            }
            catch
            {
                throw new DownloadClientException("Failed to get item status");
            }
        }
    }
}