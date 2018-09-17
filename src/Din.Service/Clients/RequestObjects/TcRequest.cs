using System.Collections.Generic;
using Newtonsoft.Json;

namespace Din.Service.Clients.RequestObjects
{
    public class TcRequest : ContentRequestObject
    {
        [JsonProperty("tvdbid")] public string TvShowId { get; set; }
        [JsonProperty("seasons")] public List<TcRequestSeason> Seasons { get; set; }
    }

    public class TcRequestSeason
    {
        [JsonProperty("seasonNumber")] public string SeasonNumber { get; set; }
        [JsonProperty("monitored")] public bool Monitored { get; set; }
    }
}