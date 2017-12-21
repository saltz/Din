using Newtonsoft.Json;

namespace DinWebsite.Models.Movie
{
    public class Image
    {
        [JsonProperty("covertype")]
        public string Covertype { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }

        public Image() { }

        public Image(string url)
        {
            Covertype = "poster";
            Url = url;
        }
    }




}
