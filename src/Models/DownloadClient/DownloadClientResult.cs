using System.Collections.Generic;
using Newtonsoft.Json;

namespace Models.DownloadClient
{
    public class DownloadClientResult
    {
        [JsonProperty("torrents")]
        public List<DownloadClientItem> Items { get; set; }
    }
}
