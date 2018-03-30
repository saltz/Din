using Newtonsoft.Json;

namespace DinWebsite.ExternalModels.DownloadClient
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
}