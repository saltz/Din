using Newtonsoft.Json;

namespace DinWebsite.Models.Movie
{
    public class AddOptions
    {
        [JsonProperty("ignoreEpisodesWithFiles")]
        public bool IgnoreEpisodesWithFiles { get; set; }
        [JsonProperty("ignoreEpisodesWithoutFiles")]
        public bool IgnoreEpisodesWithoutFiles { get; set; }
        [JsonProperty("searchForMovie")]
        public bool SearchForMovie { get; set; }
    }
}
