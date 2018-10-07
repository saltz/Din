using System.Threading.Tasks;
using Din.Service.Clients.ResponseObjects;

namespace Din.Service.Clients.Interfaces
{
    public interface IIpStackClient
    {
        Task<IpStackResponse> GetLocation(string ip);
    }
}
