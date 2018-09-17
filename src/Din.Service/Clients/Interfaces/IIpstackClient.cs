using System.Threading.Tasks;
using Din.Service.Dto.Context;

namespace Din.Service.Clients.Interfaces
{
    public interface IIpStackClient
    {
        Task<LoginLocationDto> GetLocation(string ip);
    }
}
