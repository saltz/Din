using System.Collections.Generic;
using System.Threading.Tasks;
using Din.ExternalModels.DownloadClient;

namespace Din.Service.Systems
{
    public class DownloadSystem
    {
        private readonly DownloadClient _downloadClient;

        public DownloadSystem(string url, string pwd)
        {
           _downloadClient = new DownloadClient(url, pwd);
        }

        public async Task<List<DownloadClientItem>> GetAllItemsAsync()
        {
            return await _downloadClient.GetAllItemsAsync();
        }

        public async Task<DownloadClientItem> GetItemStatusAsync(string itemHash)
        {
            return await _downloadClient.GetItemStatusAsync(itemHash);
        }
    }
}
