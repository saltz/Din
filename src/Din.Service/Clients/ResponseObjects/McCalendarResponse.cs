using System;
using Newtonsoft.Json;

namespace Din.Service.Clients.ResponseObjects
{
    public class McCalendarResponse : ContentCalendarResponse
    {
        [JsonProperty("tmdbid")]
        public int Id { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("inCinemas")]
        public DateTime InCinemas { get; set; }
        [JsonProperty("physicalRelease")]
        public DateTime PhysicalRelease { get; set; }
        [JsonProperty("downloaded")]
        public bool Downloaded { get; set; }
    }
}
