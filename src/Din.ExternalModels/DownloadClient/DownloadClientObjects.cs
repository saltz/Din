using System.Collections.Generic;
using Newtonsoft.Json;

namespace Din.ExternalModels.DownloadClient
{
    public abstract class DcRequestObject
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("method")] public string Method { get; set; }
    }

    public class DcSCollectionParam : DcRequestObject
    {
        [JsonProperty("params")] public List<List<string>> Params { get; set; }

        public DcSCollectionParam()
        {
        }

        public DcSCollectionParam(string method, List<List<string>> _params, int id)
        {
            Method = method;
            Params = _params;
            Id = id;
        }
    }

    public class DcSingleParam : DcRequestObject
    {
        [JsonProperty("params")] public List<string> Params { get; set; }

        public DcSingleParam()
        {
        }

        public DcSingleParam(string method, List<string> _params, int id)
        {
            Method = method;
            Params = _params;
            Id = id;
        }
    }

    public class DcResponseObject
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("result")] public DcResult Result { get; set; }
        [JsonProperty("error")] public object Error { get; set; }
    }

    public class DcResult
    {
        [JsonProperty("torrents")] public List<DownloadClientItem> Items { get; set; }
    }
}