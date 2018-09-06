using System.Collections.Generic;
using Newtonsoft.Json;

namespace Din.Service.Clients.RequestObjects
{
    public abstract class DCRequest
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("method")] public string Method { get; set; }
    }

    public class DCCParamCollectionRequest : DCRequest
    {
        [JsonProperty("params")] public ICollection<ICollection<string>> Params { get; set; }
    }

    public class DCSingleParamRequest : DCRequest
    {
        [JsonProperty("params")] public ICollection<string> Params { get; set; }
    }
}