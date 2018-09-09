using Newtonsoft.Json;

namespace Din.Service.Clients.ResponseObjects
{
    public class GiphyResponse
    {
        [JsonProperty("data")]
        public GiphyData Data { get; set; }
    }
    public class GiphyData
    {
        [JsonProperty("image_original_url")]
        public string ImageOriginalUrl { get; set; }
    }
}
