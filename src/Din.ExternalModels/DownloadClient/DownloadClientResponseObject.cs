using System.Collections.Generic;
using Newtonsoft.Json;

namespace Din.ExternalModels.DownloadClient
{
    public class DownloadClientResponseObject
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("result")]
        public DownloadClientResult Result { get; set; }

        [JsonProperty("error")]
        public object Error { get; set; }
    }

    public class DownloadClientResult
    {
        [JsonProperty("torrents")]
        public List<DownloadClientItem> Items { get; set; }
    }
}