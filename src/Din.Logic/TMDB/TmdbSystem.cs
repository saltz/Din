using System.Collections.Generic;
using System.Threading.Tasks;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace Din.Logic.TMDB
{
    public class TmdbSystem
    {
        private readonly TMDbClient _tmDbClient;

        public TmdbSystem(string key)
        {
            _tmDbClient = new TMDbClient(key);
        }

        public async Task<List<SearchMovie>> SearchMovie(string searchQuery)
        {
            var movies = await _tmDbClient.SearchMovieAsync(searchQuery);
            return movies.Results;
        }
    }
}
