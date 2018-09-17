using Newtonsoft.Json;

namespace Din.Service.Clients.ResponseObjects
{
    public class TcTvShowResponse : ContentCalendarResponse
    {
        [JsonProperty("title")] public string Title { get; set; }
    }
}
