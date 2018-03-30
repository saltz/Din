using Newtonsoft.Json;

namespace DinWebsite.ExternalModels.Movie
{
    public class Image
    {
        public Image()
        {
        }

        public Image(string url)
        {
            Covertype = "poster";
            Url = url;
        }

        [JsonProperty("covertype")]
        public string Covertype { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}