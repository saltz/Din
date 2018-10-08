using System.Collections.Generic;
using TMDbLib.Objects.Search;

namespace Din.ViewModels
{
    public class SearchResultViewModel<TIdentifier, TSearchType> where TSearchType : SearchBase
    {
        public IEnumerable<TIdentifier> CurrentCollection { get; set; }
        public ICollection<TSearchType> QueryCollection { get; set; }

    }
}