using System.Collections.Generic;
using System.Threading.Tasks;
using Din.Service.Concrete;
using TMDbLib.Client;
using TMDbLib.Objects.Search;

namespace Din.Service.Systems
{
    public class TmdbSystem
    {
        private readonly TMDbClient _tmDbClient;

        public TmdbSystem()
        {
            _tmDbClient = new TMDbClient(MainService.PropertyFile.Get("tmdb"));
        }

        public async Task<List<SearchMovie>> SearchMovieAsync(string searchQuery)
        {
            return (await _tmDbClient.SearchMovieAsync(searchQuery)).Results;
        }

        public async Task<List<SearchTv>> SearchTvShowAsync(string searchQuery)
        {
            return (await _tmDbClient.SearchTvShowAsync(searchQuery)).Results;
        }

        public async Task<string> GetTvShowTvdbId(int id)
        {
            var result = await _tmDbClient.GetTvShowExternalIdsAsync(id);
            return result.TvdbId;
        }

        public async Task<List<SearchTvSeason>> GetTvShowSeasons(int id)
        {
            return (await _tmDbClient.GetTvShowAsync(id)).Seasons;
        }
    }
}
