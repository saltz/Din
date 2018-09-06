using System.Threading.Tasks;
using Din.Service.Clients.Concrete;
using Din.Service.Clients.Interfaces;
using Din.Service.DTO;
using Din.Service.Services.Interfaces;

namespace Din.Service.Services.Concrete
{
    /// <inheritdoc />
    public class StatusCodeService : IStatusCodeService
    {
        private readonly IGiphyClient _client;

        public StatusCodeService(IGiphyClient client)
        {
            _client = client;
        }

        public async Task<StatusCodeDTO> GenerateDataToDisplayAsync(int statusCode)
        {
            var dto = new StatusCodeDTO
            {
                StatusCode = statusCode
            };
            switch (statusCode)
            {
                case 400:
                    dto.StatusMessage = "Te Fck did you do";
                    dto.Gif =  await _client.GetRandomGifAsync(GiphyTag.Trending);
                    break;
                case 401:
                    dto.StatusMessage = "You're not supposed to do that";
                    dto.Gif = await _client.GetRandomGifAsync(GiphyTag.Nicetry); 
                    break;
                case 403:
                    dto.StatusMessage = "No No No";
                    dto.Gif = await _client.GetRandomGifAsync(GiphyTag.Nicetry);
                    break;
                case 404:
                    dto.StatusMessage = "It's gone";
                    dto.Gif = await _client.GetRandomGifAsync(GiphyTag.Funny);
                    break;
                case 408:
                    dto.StatusMessage = "The server timed out waiting for the request";
                    dto.Gif = await _client.GetRandomGifAsync(GiphyTag.Error);
                    break;
                case 500:
                    dto.StatusMessage = "Hmmm seems like I fucked up";
                    dto.Gif = await _client.GetRandomGifAsync(GiphyTag.Error);
                    break;
                default:
                    dto.StatusMessage = "Hmmm seems like I fucked up";
                    dto.Gif = await _client.GetRandomGifAsync(GiphyTag.Error);
                    break;
            }
            return dto;
        }
    }
}
