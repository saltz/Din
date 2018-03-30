using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace DinWebsite.ExternalModels.Content
{
    public class Urls
    {
        [JsonProperty("full")]
        public string Full { get; set; }
        [JsonProperty("regular")]
        public string Regular { get; set; }
        [JsonProperty("custom")]
        public string Custom { get; set; }
    }
}
