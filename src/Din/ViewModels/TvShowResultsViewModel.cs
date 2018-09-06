using System.Collections.Generic;
using TMDbLib.Objects.Search;

namespace Din.ViewModels
{
    public class TvShowResultsViewModel
    {
        public IEnumerable<string> CurrentTvShowCollection { get; set; }
        public ICollection<SearchTv> QueryCollection { get; set; }
    }
}