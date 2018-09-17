using Newtonsoft.Json;

namespace Din.Service.Clients.ResponseObjects
{
    public abstract class ContentCalendarResponse
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("overview")]
        public string Overview { get; set; }
        [JsonProperty("hasFile")]
        public bool HasFile { get; set; }
    }
}
