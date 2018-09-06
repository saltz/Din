using System.Collections.Generic;
using TMDbLib.Objects.Search;

namespace Din.Service.DTO.Content
{
    public class MovieDTO
    {
        public IEnumerable<int> CurrentMovieCollection { get; set; }
        public ICollection<SearchMovie> QueryCollection { get; set; }
    }
}
