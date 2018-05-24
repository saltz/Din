using System.Threading.Tasks;

namespace Din.ExternalModels.TvtbClient
{
    /// <summary>
    /// Scrapper for tvdb information.
    /// </summary>
    public interface ITvdbClient
    {
        /// <summary>
        /// Search Series with one parameter.
        /// </summary>
        /// <param name="query">The title or parts of it.</param>
        /// <returns>A collection containing series.</returns>
        Task<TvdbRootObject> SearchShowAsync(string query);
    }
}