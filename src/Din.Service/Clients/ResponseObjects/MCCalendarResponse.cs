﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Din.Service.Clients.ResponseObjects
{
    public class MCCalendarResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("inCinemas")]
        public DateTime InCinemas { get; set; }
        [JsonProperty("physicalRelease")]
        public DateTime PhysicalRelease { get; set; }
        [JsonProperty("downloaded")]
        public bool Downloaded { get; set; }
    }
}