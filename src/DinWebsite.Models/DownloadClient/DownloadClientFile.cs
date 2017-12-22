using Newtonsoft.Json;

namespace DinWebsite.ExternalModels.DownloadClient
{
    public class DownloadClientFile
    {
        [JsonProperty("index")]
        public double Index { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("offset")]
        public double Offset { get; set; }

        [JsonProperty("size")]
        public double Size { get; set; }
    }
}
