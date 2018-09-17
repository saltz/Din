using System.Collections.Generic;
using Din.Service.Clients.ResponseObjects;

namespace Din.Service.Dto
{
    public class MediaDto
    {
        public ICollection<UnsplashResponseObject> BackgroundImageCollection { get; set; }
        public GiphyResponse Gif { get; set; }
    }
}
