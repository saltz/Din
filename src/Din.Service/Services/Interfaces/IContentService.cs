using System.Threading.Tasks;
using TMDbLib.Objects.Search;

namespace Din.Service.Services.Interfaces
{
    /// <summary>
    /// ContentService for the corresponding controller.
    /// </summary>
    public interface IContentService
    {
        /// <summary>
        /// Search movies with a specific query.
        /// </summary>
        /// <param name="query">The movie title or a part of it.</param>
        /// <returns>ViewModel containing collections of existing movies and query results.</returns>
        Task<MovieResultsViewModel> SearchMovieAsync(string query);
        /// <summary>
        /// Search Tv Shows with a specific query.
        /// </summary>
        /// <param name="query">The Tv Show title or a part of it.</param>
        /// <returns>ViewModel containing collections of existing movies and query results.</returns>
        Task<TvShowResultsViewModel> SearchTvShowAsync(string query);
        /// <summary>
        /// Adds movie to the system.
        /// </summary>
        /// <param name="movie">The movie object that needs to be added.</param>
        /// <param name="id">The account id of the current session.</param>
        /// <returns>The status result.</returns>
        Task<ResultViewModel> AddMovieAsync(SearchMovie movie, int id);
        /// <summary>
        /// Adds TvShow to the system.
        /// </summary>
        /// <param name="tvShow">The TvShow object that needs to be added.</param>
        /// <param name="id">The account id of the current session.</param>
        /// <returns>The status result.</returns>
        Task<ResultViewModel> AddTvShowAsync(SearchTv tvShow, int id);
    }
}
