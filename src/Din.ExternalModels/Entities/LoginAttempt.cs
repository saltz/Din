using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Din.ExternalModels.Entities
{
    public class LoginAttempt
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Device { get; set; }
        public string Os { get; set; }
        public string Browser { get; set; }
        public string PublicIp { get; set; }
        public LoginLocation Location { get; set; }
        public DateTime DateAndTime { get; set; }
        public LoginStatus Status { get; set; }

        public LoginAttempt()
        {
        }

        public LoginAttempt(string username, string device, string os, string browser, string publicIp,
            LoginLocation location, DateTime dateAndTime, LoginStatus status)
        {
            Username = username;
            Device = device;
            Os = os;
            Browser = browser;
            PublicIp = publicIp;
            Location = location;
            DateAndTime = dateAndTime;
            Status = status;
        }
    }

    public class LoginLocation
    {
        public int ID { get; set; }
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

        public LoginLocation()
        {
        }

        public async Task<LoginLocation> QueryGeographicalLocationAsync(string url)
        {
            var client = new HttpClient();
            var result = await client.GetStringAsync(url);
            return JsonConvert.DeserializeObject<LoginLocation>(result);
        }
    }


    public enum LoginStatus
    {
        Failed,
        Success
    }
}