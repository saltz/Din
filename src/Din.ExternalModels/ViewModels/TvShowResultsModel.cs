using System.Collections.Generic;
using Din.ExternalModels.TvtbClient;
using TMDbLib.Objects.Search;

namespace Din.ExternalModels.ViewModels
{
    public class TvShowResultsModel
    {
        public TvdbRootObject QueryResult { get; set; }
        public List<int> CurrentIdList { get; set; }
    }
}