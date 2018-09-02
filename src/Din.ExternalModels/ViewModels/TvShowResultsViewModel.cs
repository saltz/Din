using System.Collections.Generic;
using TMDbLib.Objects.Search;

namespace Din.ExternalModels.ViewModels
{
    public class TvShowResultsViewModel
    {
        public List<SearchTv> QueryResult { get; set; }
        public List<string> CurrentTtileList { get; set; }
    }
}