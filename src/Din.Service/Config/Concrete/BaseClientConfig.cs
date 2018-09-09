namespace Din.Service.Config.Concrete
{
    public abstract class BaseClientConfig
    {
        public string Url { get; }
        public string Key { get; }

        public BaseClientConfig(string url, string key)
        {
            Url = url;
            Key = key;
        }
    }
}
