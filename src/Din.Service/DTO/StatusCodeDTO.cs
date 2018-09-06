using Din.Service.Clients.ResponseObjects;

namespace Din.Service.DTO
{
    public class StatusCodeDTO
    {
        public GiphyResponse Gif { get; set; }
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
    }
}
