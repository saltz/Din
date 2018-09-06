using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Din.Service.Clients.Concrete;
using Din.Service.Clients.Interfaces;
using Din.Service.Clients.ResponseObjects;
using Din.Service.DTO;
using Din.Service.Services.Interfaces;

namespace Din.Service.Services.Concrete
{
    public class MediaService : IMediaService
    {
        private readonly IGiphyClient _giphyClient;
        private readonly IUnsplashClient _unsplashClient;

        private DateTime _creationDate;
        private ICollection<UnsplashResponseObject> _imageCollection;

        public MediaService(IGiphyClient giphyClient, IUnsplashClient unsplashClient)
        {
            _giphyClient = giphyClient;
            _unsplashClient = unsplashClient;
            _creationDate = DateTime.Now;
        }

        public async Task<MediaDTO> GenerateBackgroundImages()
        {
            if (_imageCollection == null || _creationDate.AddDays(1) < DateTime.Now)
            {
                _creationDate = DateTime.Now;
                _imageCollection = await _unsplashClient.GetBackgroundCollection();
            }

            return new MediaDTO
            {
                BackgroundImageCollection = _imageCollection
            };
        }

        public async Task<MediaDTO> GenerateGif(GiphyTag tag)
        {
            return new MediaDTO
            {
                Gif = await _giphyClient.GetRandomGifAsync(tag)
            };
        }
    }
}