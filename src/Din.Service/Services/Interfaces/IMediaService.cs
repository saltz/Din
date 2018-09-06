using System.Threading.Tasks;
using Din.Service.Clients.Concrete;
using Din.Service.DTO;

namespace Din.Service.Services.Interfaces
{
    public interface IMediaService
    {
        Task<MediaDTO> GenerateBackgroundImages();
        Task<MediaDTO> GenerateGif(GiphyTag tag);
    }
}
