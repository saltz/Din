using System.Collections.Generic;
using TMDbLib.Objects.Search;

namespace Din.ExternalModels.ViewModels
{
    public class MovieResultsViewModel
    {
        public ICollection<SearchMovie> QueryResult { get; set; }
        public ICollection<int> CurrentIdList { get; set; }
    }
}