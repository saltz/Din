using System;
using Din.Service.Clients.RequestObjects;
using Newtonsoft.Json;

namespace Din.Service.Clients.ResponseObjects
{
    public class TCCalendarResponse
    {
        [JsonProperty("seasonNumber")]
        public int SeasonNumber { get; set; }
        [JsonProperty("episodeNumber")]
        public int EpisodeNumber { get; set; }
        [JsonProperty("airDate")]
        public DateTime AirDate { get; set; }
        [JsonProperty("airDateUtc")]
        public DateTime AirDateUtc { get; set; }
        [JsonProperty("series")]
        public TCRequest Series { get; set; }
    }
}
