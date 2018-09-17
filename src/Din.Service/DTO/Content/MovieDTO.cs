using System.Collections.Generic;
using TMDbLib.Objects.Search;

namespace Din.Service.Dto.Content
{
    public class MovieDto
    {
        public IEnumerable<int> CurrentMovieCollection { get; set; }
        public ICollection<SearchMovie> QueryCollection { get; set; }
    }
}
