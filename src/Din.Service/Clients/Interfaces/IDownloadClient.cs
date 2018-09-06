using System.Threading.Tasks;
using Din.Service.Clients.ResponseObjects;

namespace Din.Service.Clients.Interfaces
{
    public interface IDownloadClient
    {
        Task<DCResponse> GetAllItemsAsync();
        Task<DCResponseItem> GetItemStatusAsync(string itemHash);
    }
}
