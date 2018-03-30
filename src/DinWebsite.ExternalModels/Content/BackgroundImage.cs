using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace DinWebsite.ExternalModels.Content
{
    public class BackgroundImage
    {
        [JsonProperty("urls")]
        public Urls Urls { get; set; }
    }
}
