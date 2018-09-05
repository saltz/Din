using System.Collections.Generic;
using TMDbLib.Objects.Search;

namespace Din.ViewModels
{
    public class TvShowResultsViewModel
    {
        public IEnumerable<SearchTv> QueryResult { get; set; }
        public IEnumerable<string> CurrentTitleList { get; set; }
    }
}