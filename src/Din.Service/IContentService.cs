using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Din.ExternalModels.Entities;
using Din.ExternalModels.ViewModels;
using TMDbLib.Objects.Search;

namespace Din.Service
{
    public interface IContentService
    {
        Task<SearchResultsModel> SearchMovieAsync(string query);
        Task<AddContentResultModel> AddMovieAsync(SearchMovie movie, Account account);
    }
}
