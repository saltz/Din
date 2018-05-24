using System.Collections.Generic;
using TMDbLib.Objects.Search;

namespace Din.ExternalModels.ViewModels
{
    public class MovieResultsModel
    {
        public List<SearchMovie> QueryResult { get; set; }
        public List<int> CurrentIdList { get; set; }
    }
}