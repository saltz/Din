using System.Threading.Tasks;
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
        /// <param name="query">query</param>
        /// <returns>Model containing two sets of collections, one with results of the query the otherone contains the current movies.</returns>
        Task<MovieResultsViewModel> SearchMovieAsync(string query);
        /// <summary>
        /// Search Tv Shows with a specific query.
        /// </summary>
        /// <param name="query">The Tv Show title or parts of it.</param>
        /// <returns>Model containing two sets of collections, one with the results of the query the otherone the current Tv Shows on the system.</returns>
        Task<TvShowResultsViewModel> SearchTvShowAsync(string query);
        /// <summary>
        /// Adds movie to the system.
        /// </summary>
        /// <param name="movie">The movie object that needs to be added.</param>
        /// <param name="id">The Id of the account that performs the action.</param>
        /// <returns>The status results of the action (ok/bad).</returns>
        Task<ResultViewModel> AddMovieAsync(SearchMovie movie, int id);
        /// <summary>
        /// Adds TvShow to the system.
        /// </summary>
        /// <param name="tvShow">The TvShow object that needs to be added.</param>
        /// <param name="id">The Id of the account that performs the action.</param>
        /// <returns>The status results of the action (ok/bad).</returns>
        Task<ResultViewModel> AddTvShowAsync(SearchTv tvShow, int id);
    }
}
