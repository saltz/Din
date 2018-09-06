using Newtonsoft.Json;

namespace Din.Service.Clients.RequestObjects
{
    public class MCRequest : ContentRequestObject
    {
        [JsonProperty("year")] public int Year { get; set; }
        [JsonProperty("tmdbid")] public int TmdbId { get; set; }
        [JsonProperty("downloaded")] public bool Downloaded { get; set; }
        [JsonProperty("addOptions")] public MCRequestOptions MovieOptions { get; set; }
    }

    public class MCRequestOptions
    {
        [JsonProperty("ignoreEpisodesWithFiles")]
        public bool IgnoreEpisodesWithFiles { get; set; }

        [JsonProperty("ignoreEpisodesWithoutFiles")]
        public bool IgnoreEpisodesWithoutFiles { get; set; }

        [JsonProperty("searchForMovie")] public bool SearchForMovie { get; set; }
    }
}