using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TMDbLib.Client;
using TMDbLib.Objects.Search;

namespace DinWebsite.Logic.TMDB
{
    public class TmdbSystem
    {
        private readonly TMDbClient TmDbClient;

        public TmdbSystem()
        {
            TmDbClient = new TMDbClient(File.ReadLines("C:/din_properties/api_key").First());
        }

        public List<SearchMovie> SearchMovie(string searchQuery)
        {
            var movies = TmDbClient.SearchMovieAsync(searchQuery).Result;
            return movies.Results;
        }
    }
}
