using System.Threading.Tasks;
using Din.Service.DTO.Context;

namespace Din.Service.Clients.Interfaces
{
    public interface IIpStackClient
    {
        Task<LoginLocationDTO> GetLocation(string ip);
    }
}
