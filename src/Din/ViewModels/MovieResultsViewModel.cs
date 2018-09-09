using System.Collections.Generic;
using TMDbLib.Objects.Search;

namespace Din.ViewModels
{
    public class MovieResultsViewModel
    {
        public IEnumerable<int> CurrentMovieCollection { get; set; }
        public ICollection<SearchMovie> QueryCollection { get; set; }

    }
}