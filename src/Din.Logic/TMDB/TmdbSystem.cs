using System.Collections.Generic;
using TMDbLib.Client;
using TMDbLib.Objects.Search;

namespace Din.Logic.TMDB
{
    public class TmdbSystem
    {
        private readonly TMDbClient TmDbClient;

        public TmdbSystem(string key)
        {
            TmDbClient = new TMDbClient(key);
        }

        public List<SearchMovie> SearchMovie(string searchQuery)
        {
            var movies = TmDbClient.SearchMovieAsync(searchQuery).Result;
            return movies.Results;
        }
    }
}
