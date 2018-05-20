using System.Threading.Tasks;
using Din.ExternalModels.Entities;
using Din.ExternalModels.ViewModels;
using TMDbLib.Objects.Search;

namespace Din.Service.Interfaces
{
    public interface IContentService
    {
        Task<SearchResultsModel> SearchMovieAsync(string query);
        Task<AddContentResultModel> AddMovieAsync(SearchMovie movie, Account account);
    }
}
