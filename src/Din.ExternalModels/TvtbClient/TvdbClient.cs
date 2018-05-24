using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Din.ExternalModels.Utils;
using Newtonsoft.Json;

namespace Din.ExternalModels.TvtbClient
{
    /// <inheritdoc />
    public class TvdbClient : ITvdbClient
    {
        private readonly string _apiKey;
        private readonly string _userKey;
        private readonly string _username;

        public TvdbClient(string apiKey, string userKey, string username)
        {
            _apiKey = apiKey;
            _userKey = userKey;
            _username = username;
        }


        public async Task<string> Authenticate()
        {
            const string url = "https://api.thetvdb.com/login";
            var payload = JsonConvert.SerializeObject(new
            {
                apikey = _apiKey,
                userkey = _userKey,
                usernam = _username
            });
            var result = await new HttpRequestHelper(url, false).PerformPostRequestAsync(payload);
            return !result.Item1.Equals(200) ? null : JsonConvert.DeserializeObject<Dictionary<string, string>>(result.Item2)["token"];
        }

        public Task<List<TvdbObject>> SearchSeries(string query, string token)
        {
            const string url = "https://api.thetvdb.com/search/series?name={0}";
            var result = await new HttpRequestHelper(url,);
        }
    }
}
