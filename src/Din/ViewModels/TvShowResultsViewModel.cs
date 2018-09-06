using System.Collections.Generic;
using TMDbLib.Objects.Search;

namespace Din.ViewModels
{
    public class TvShowResultsViewModel
    {
        public ICollection<SearchTv> QueryResult { get; set; }
        public IEnumerable<string> CurrentTitleList { get; set; }
    }
}