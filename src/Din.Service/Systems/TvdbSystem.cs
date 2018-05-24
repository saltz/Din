using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Din.ExternalModels.TvtbClient;
using Din.Service.Classes;
using TMDbLib.Objects.Search;

namespace Din.Service.Systems
{
    public class TvdbSystem
    {
        private readonly TvdbClient _tvdbClient;

        public TvdbSystem()
        {
            _tvdbClient = new TvdbClient(MainService.PropertyFile.get("tvdb-apikey"),
                MainService.PropertyFile.get("tvdb-userkey"),
                MainService.PropertyFile.get("tvdb-username"));
        }

        public async Task<TvdbRootObject> SearchTvShowAsync(string searchQuery)
        {
            return await _tvdbClient.SearchShowAsync(searchQuery);
        }
    }
}
