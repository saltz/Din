using System.Collections.Generic;
using Newtonsoft.Json;

namespace Din.Service.Clients.RequestObjects
{
    public class TCRequest : ContentRequestObject
    {
        [JsonProperty("tvdbid")] public string TvShowId { get; set; }
        [JsonProperty("seasons")] public List<TCRequestSeason> Seasons { get; set; }
    }

    public class TCRequestSeason
    {
        [JsonProperty("seasonNumber")] public string SeasonNumber { get; set; }
        [JsonProperty("monitored")] public bool Monitored { get; set; }
    }
}