using System.Collections.Generic;
using TMDbLib.Objects.Search;

namespace Din.ExternalModels.ViewModels
{
    public class TvShowResultsViewModel
    {
        public ICollection<SearchTv> QueryResult { get; set; }
        public ICollection<string> CurrentTitleList { get; set; }
    }
}