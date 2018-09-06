using System.Collections.Generic;
using TMDbLib.Objects.Search;

namespace Din.ViewModels
{
    public class MovieResultsViewModel
    {
        public ICollection<SearchMovie> QueryResult { get; set; }
        public IEnumerable<int> CurrentIdList { get; set; }
    }
}