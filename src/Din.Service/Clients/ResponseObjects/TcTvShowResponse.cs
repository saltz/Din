using Newtonsoft.Json;

namespace Din.Service.Clients.ResponseObjects
{
    public class TcTvShowResponse
    {
        [JsonProperty("tvdbId")] public int Id { get; set; }

        [JsonProperty("title")] public string Title { get; set; }
        [JsonProperty("status")] public string Status { get; set; }
    }
}
