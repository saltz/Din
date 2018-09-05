using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Din.ExternalModels.MediaSystem;
using Din.ExternalModels.Utils;
using Din.Service.Clients.Interfaces;
using Newtonsoft.Json;

namespace Din.Service.Clients.Concrete
{
    public class TvShowClient : ITvShowClient
    {
        public async Task<List<string>> GetCurrentTvShowsAsync()
        {
            var tvShowIds = new List<string>();
            var objects =
                JsonConvert.DeserializeObject<List<MediaSystemTvShow>>(
                    await new HttpRequestHelper(_url, false).PerformGetRequestAsync());
            foreach (var t in objects)
                tvShowIds.Add(t.Title.ToLower());
            return tvShowIds;
        }
    }
}
