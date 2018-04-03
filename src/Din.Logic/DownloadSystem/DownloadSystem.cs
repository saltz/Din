using Din.ExternalModels.DownloadClient;

namespace Din.Logic.DownloadSystem
{
    public class DownloadSystem
    {
        private readonly DownloadClient _downloadClient;

        public DownloadSystem(string url, string pwd)
        {
           _downloadClient = new DownloadClient(url, pwd);
        }

    }
}
