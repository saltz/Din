using System.Collections.Generic;
using Newtonsoft.Json;

namespace Din.Service.Clients.ResponseObjects
{
    public class TcTvShowResponse
    {
        [JsonProperty("tvdbId")] public int Id { get; set; }

        [JsonProperty("title")] public string Title { get; set; }
        [JsonProperty("status")] public string Status { get; set; }
        [JsonProperty("seasons")] public ICollection<TcTvShowResponseSeason> Seasons { get; set; }


    }

    public class TcTvShowResponseSeason
    {
        [JsonProperty("seasonNumber")] public int SeasonsNumber { get; set; }
        [JsonProperty("statistics")] public TcTvShowResponseSeasonStatistics Statistics { get; set; }     
    }

    public class TcTvShowResponseSeasonStatistics
    {
        [JsonProperty("episodeCount")] public string EpisodeCount { get; set; }
        [JsonProperty("totalEpisodeCount")] public string TotalEpisodeCount { get; set; }
    }
}
