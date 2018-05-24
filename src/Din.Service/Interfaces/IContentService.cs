using System.Threading.Tasks;
using Din.ExternalModels.Entities;
using Din.ExternalModels.ViewModels;
using TMDbLib.Objects.Search;

namespace Din.Service.Interfaces
{
    /// <summary>
    /// ContentService for the corresponding controller.
    /// </summary>
    public interface IContentService
    {
        /// <summary>
        /// Search movies with a specific query.
        /// </summary>
        /// <param name="query">.</param>
        /// <returns>Model containing two sets of collections, one with results of the query the otherone contains the current movies.</returns>
        Task<MovieResultsModel> SearchMovieAsync(string query);
        /// <summary>
        /// Search Tv Shows with a specific query.
        /// </summary>
        /// <param name="query">The Tv Show title or parts of it.</param>
        /// <returns>Model containing two sets of collections, one with the results of the query the otherone the current Tv Shows on the system.</returns>
        Task<TvShowResultsModel> SearchTvShowAsync(string query);
        /// <summary>
        /// Adds movie to the system.
        /// </summary>
        /// <param name="movie">The movie object that needs to be added.</param>
        /// <param name="account">The account that performs the action.</param>
        /// <returns>The status results of the action (ok/bad).</returns>
        Task<AddContentResultModel> AddMovieAsync(SearchMovie movie, Account account);
        /// <summary>
        /// Adds TvShow to the system.
        /// </summary>
        /// <param name="tvShow">The TvShow object that needs to be added.</param>
        /// <param name="account">The account that performs the action.</param>
        /// <returns>The status results of the action (ok/bad).</returns>
        Task<AddContentResultModel> AddTvShowAsync(string tvShow, Account account);
    }
}
