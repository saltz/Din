using Din.Service.Config.Interfaces;

namespace Din.Service.Config.Concrete
{
    public class DownloadClientConfig : BaseClientConfig, IDownloadClientConfig
    {
        public string Password { get; }

        public DownloadClientConfig(string url, string password) : base(url, null)
        {
            Password = password;
        }

    }
}
