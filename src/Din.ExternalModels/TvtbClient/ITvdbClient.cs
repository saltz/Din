using System.Collections.Generic;
using System.Threading.Tasks;

namespace Din.ExternalModels.TvtbClient
{
    /// <summary>
    /// Scrapper for tvdb information.
    /// </summary>
    public interface ITvdbClient
    {
        /// <summary>
        /// Authenticates against the API.
        /// </summary>
        /// <returns>Valid JWT Token for future requests</returns>
        Task<string> Authenticate();
        /// <summary>
        /// Search Series with one parameter.
        /// </summary>
        /// <param name="query">The title or parts of it.</param>
        /// <returns>A collection containing series.</returns>
        Task<List<TvdbObject>> SearchSeries(string query);
    }
}