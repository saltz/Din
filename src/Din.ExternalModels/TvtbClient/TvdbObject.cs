using System;
using Newtonsoft.Json;

namespace Din.ExternalModels.TvtbClient
{
    public class TvdbObject
    {
        [JsonProperty("id")]
        public int SeriesId { get; set; }
        [JsonProperty("seriesName")]
        public string Title { get; set; }
        [JsonProperty("banner")]
        public string Banner { get; set; }
        [JsonProperty("firstAired")]
        public DateTime FirstAired { get; set; }
        [JsonProperty("network")]
        public string Network { get; set; }
        [JsonProperty("overview")]
        public string Overview { get; set; }
    }
}