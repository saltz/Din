using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Models.DownloadClient
{
    public class DownloadClientResponseObject
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("result")]
        public DownloadClientResult Result { get; set; }
        [JsonProperty("error")]
        public object Error { get; set; }
    }
}
