using System;
using Din.Service.Clients.RequestObjects;
using Newtonsoft.Json;

namespace Din.Service.Clients.ResponseObjects
{
    public class TcCalendarResponse
    {
        [JsonProperty("seasonNumber")]
        public int SeasonNumber { get; set; }
        [JsonProperty("episodeNumber")]
        public int EpisodeNumber { get; set; }
        [JsonProperty("airDate")]
        public DateTime AirDate { get; set; }
        [JsonProperty("airDateUtC")]
        public DateTime AirDateUtC { get; set; }
        [JsonProperty("series")]
        public TcRequest Series { get; set; }
    }
}
