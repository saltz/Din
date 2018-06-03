using Newtonsoft.Json;

namespace Din.ExternalModels.GeneratedMedia
{
    public class UnsplashContainer
    {
        [JsonProperty("urls")]
        public Urls Urls { get; set; }
    }

    public class Urls
    {
        [JsonProperty("full")]
        public string Full { get; set; }
        [JsonProperty("regular")]
        public string Regular { get; set; }
    }
}
