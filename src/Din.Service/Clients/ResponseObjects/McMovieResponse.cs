using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Din.Service.Clients.ResponseObjects
{
    class McMovieResponse
    {
        [JsonProperty("tmdbid")] public int Id { get; set; }
    }
}
