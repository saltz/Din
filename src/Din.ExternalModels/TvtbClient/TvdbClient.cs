using System.Collections.Generic;
using System.Threading.Tasks;
using Din.ExternalModels.Utils;
using Newtonsoft.Json;

namespace Din.ExternalModels.TvtbClient
{
    /// <inheritdoc />
    public class TvdbClient : ITvdbClient
    {
        private readonly string _jwtToken;
        

        public TvdbClient(string apiKey, string userKey, string username)
        {
            _jwtToken = AuthenticateAsync(apiKey, userKey, username).Result;
        }


        private static async Task<string> AuthenticateAsync(string apiKey, string userKey, string username)
        {
            const string url = "https://tvdb2.plex.tv/login";
            var payload = JsonConvert.SerializeObject(new TvdbCredentials()
            {
                ApiKey = apiKey,
                UserKey = userKey,
                Username = username
            });
            var result = await new HttpRequestHelper(url, false).PerformPostRequestAsync(payload);
            return !result.Item1.Equals(200) ? null : JsonConvert.DeserializeObject<Dictionary<string, string>>(result.Item2)["token"];
        }

        public async Task<TvdbRootObject> SearchShowAsync(string query)
        {
            var url = "https://tvdb2.plex.tv/search/series?name=" + query;
            return JsonConvert.DeserializeObject<TvdbRootObject>(await new HttpRequestHelper(url, _jwtToken).PerformGetRequestAsync());
        }
    }
}
