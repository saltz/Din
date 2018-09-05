using System.Collections.Generic;
using System.Threading.Tasks;
using Din.ExternalModels.DownloadClient;

namespace Din.Service.Clients.Interfaces
{
    public interface IDownloadClient
    {
        Task<ICollection<DownloadClientItem>> GetAllItemsAsync();
        Task<DownloadClientItem> GetItemStatusAsync(string itemHash);
    }
}
