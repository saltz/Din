using System.Threading.Tasks;
using Din.Service.Clients.Concrete;
using Din.Service.Dto;

namespace Din.Service.Services.Interfaces
{
    public interface IMediaService
    {
        Task<MediaDto> GenerateBackgroundImages();
        Task<MediaDto> GenerateGif(GiphyTag tag);
    }
}
