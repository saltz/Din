using System.Collections.Generic;
using System.Threading.Tasks;
using Din.Service.Classes;
using TMDbLib.Client;
using TMDbLib.Objects.Search;

namespace Din.Service.Systems
{
    public class TmdbSystem
    {
        private readonly TMDbClient _tmDbClient;

        public TmdbSystem()
        {
            _tmDbClient = new TMDbClient(MainService.PropertyFile.get("tmdb"));
        }

        public async Task<List<SearchMovie>> SearchMovie(string searchQuery)
        {
            var movies = await _tmDbClient.SearchMovieAsync(searchQuery);
            return movies.Results;
        }
    }
}
