using Newtonsoft.Json;

namespace Din.Service.Clients.ResponseObjects
{
    public class TCTvShowResponse
    {
        [JsonProperty("title")] public string Title { get; set; }
    }
}
