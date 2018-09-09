using System;
using System.Collections.Generic;
using System.Text;
using Din.Service.Clients.ResponseObjects;

namespace Din.Service.DTO
{
    public class MediaDTO
    {
        public ICollection<UnsplashResponseObject> BackgroundImageCollection { get; set; }
        public GiphyResponse Gif { get; set; }
    }
}
