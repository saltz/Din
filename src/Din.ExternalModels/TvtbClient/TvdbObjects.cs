using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Din.ExternalModels.TvtbClient
{

    public class TvdbRootObject
    {
        [JsonProperty("data")]
        public List<TvdbShow> Shows { get; set; }
    }

    public class TvdbShow
    {
        [JsonProperty("id")]
        public int ShowId { get; set; }
        [JsonProperty("seriesName")]
        public string Title { get; set; }
        [JsonProperty("banner")]
        public string Banner { get; set; }
        [JsonProperty("firstAired")]
        public DateTime? FirstAired { get; set; }
        [JsonProperty("network")]
        public string Network { get; set; }
        [JsonProperty("overview")]
        public string Overview { get; set; }
    }

    public class TvdbCredentials
    {
        [JsonProperty("apikey")]
        public string ApiKey { get; set; }
        [JsonProperty("userkey")]
        public string UserKey { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
    }
}