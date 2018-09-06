using System.Collections.Generic;
using TMDbLib.Objects.Search;

namespace Din.Service.DTO.Content
{
    public class TvShowDTO
    {
        public IEnumerable<string> CurrentTvShowCollection { get; set; }
        public ICollection<SearchTv> QueryCollection { get; set; }
    }
}
