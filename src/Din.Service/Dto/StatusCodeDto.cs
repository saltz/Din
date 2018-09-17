using Din.Service.Clients.ResponseObjects;

namespace Din.Service.Dto
{
    public class StatusCodeDto
    {
        public GiphyResponse Gif { get; set; }
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
    }
}
