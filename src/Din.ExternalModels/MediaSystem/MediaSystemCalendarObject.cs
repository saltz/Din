using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Din.ExternalModels.MediaSystem
{
    public abstract class MediaSystemCalendarObject
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("overview")]
        public string Overview { get; set; }
        [JsonProperty("hasFile")]
        public bool HasFile { get; set; }
    }

    public class MediaSystemTvShowCalendarObject : MediaSystemCalendarObject
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
        public MediaSystemTvShow Series { get; set; }
    }

    public class MediaSystemMovieCalendarObject : MediaSystemCalendarObject
    {
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
