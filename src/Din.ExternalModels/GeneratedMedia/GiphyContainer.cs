using Newtonsoft.Json;

namespace Din.ExternalModels.GeneratedMedia
{
    public class GiphyContainer
    {
        [JsonProperty("data")]
        public GiphyData Data { get; set; }
    }

    public class GiphyData
    {
        [JsonProperty("image_original_url")]
        public string ImageOriginalUrl { get; set; }
    }

    public enum GiphyQuery
    {
        Logout,
        PageNotFound,
        Forbidden,
        ServerError,
        Random
    }

}
