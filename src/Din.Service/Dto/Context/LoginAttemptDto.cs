using System;
using Din.Data.Entities;
using Newtonsoft.Json;

namespace Din.Service.Dto.Context
{
    public class LoginAttemptDto
    {
        public string Username { get; set; }
        public string Device { get; set; }
        public string Os { get; set; }
        public string Browser { get; set; }
        public string PublicIp { get; set; }
        public LoginLocationDto Location { get; set; }
        public DateTime DateAndTime { get; set; }
        public LoginStatus Status { get; set; }
    }

    public class LoginLocationDto
    {
        [JsonProperty("continent_code")]
        public string ContinentCode { get; set; }
        [JsonProperty("continent_name")]
        public string ContinentName { get; set; }
        [JsonProperty("country_code")]
        public string CountryCode { get; set; }
        [JsonProperty("country_name")]
        public string CountryName { get; set; }
        [JsonProperty("region_code")]
        public string RegionCode { get; set; }
        [JsonProperty("region_name")]
        public string RegionName { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("zip")]
        public string ZipCode { get; set; }
        [JsonProperty("latitude")]
        public string Latitude { get; set; }
        [JsonProperty("longitude")]
        public string Longitude { get; set; }
    }
}
