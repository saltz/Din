using System.Collections.Generic;
using TMDbLib.Objects.Search;

namespace Din.Service.Dto.Content
{
    public class TvShowDto
    {
        public IEnumerable<string> CurrentTvShowCollection { get; set; }
        public ICollection<SearchTv> QueryCollection { get; set; }
    }
}
