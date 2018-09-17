using System.Collections.Generic;
using Newtonsoft.Json;

namespace Din.Service.Clients.RequestObjects
{
    public abstract class DcRequest
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("method")] public string Method { get; set; }
    }

    public class DcParamCollectionRequest : DcRequest
    {
        [JsonProperty("params")] public ICollection<ICollection<string>> Params { get; set; }
    }

    public class DcSingleParamRequest : DcRequest
    {
        [JsonProperty("params")] public ICollection<string> Params { get; set; }
    }
}