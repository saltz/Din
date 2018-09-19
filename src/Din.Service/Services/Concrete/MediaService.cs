using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Din.Service.Clients.Concrete;
using Din.Service.Clients.Interfaces;
using Din.Service.Clients.ResponseObjects;
using Din.Service.Dto;
using Din.Service.Services.Interfaces;

namespace Din.Service.Services.Concrete
{
    public class MediaService : IMediaService
    {
        private readonly IGiphyClient _giphyClient;
        private readonly IUnsplashClient _unsplashClient;
        private (DateTime DateGenerated, ICollection<UnsplashResponseObject> Collection) _unsplashData;

        public MediaService(IGiphyClient giphyClient, IUnsplashClient unsplashClient)
        {
            _giphyClient = giphyClient;
            _unsplashClient = unsplashClient;
            _unsplashData.DateGenerated = DateTime.Now;
        }

        public async Task<MediaDto> GenerateBackgroundImages()
        {
            if (_unsplashData.Collection == null || _unsplashData.DateGenerated.AddDays(1) <= DateTime.Now)
            {
                _unsplashData.DateGenerated = DateTime.Now;
                _unsplashData.Collection = await _unsplashClient.GetBackgroundCollection();
            }

            return new MediaDto
            {
                BackgroundImageCollection = _unsplashData.Collection
            };
        }

        public async Task<MediaDto> GenerateGif(GiphyTag tag)
        {
            return new MediaDto
            {
                Gif = await _giphyClient.GetRandomGifAsync(tag)
            };
        }
    }
}