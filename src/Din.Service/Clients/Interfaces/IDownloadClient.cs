using System.Threading.Tasks;
using Din.Service.Clients.ResponseObjects;

namespace Din.Service.Clients.Interfaces
{
    public interface IDownloadClient
    {
        Task<DcResponse> GetAllItemsAsync();
        Task<DcResponseItem> GetItemStatusAsync(string itemHash);
    }
}
