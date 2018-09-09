using System.Collections.Generic;
using System.Threading.Tasks;
using Din.Service.Clients.RequestObjects;

namespace Din.Service.Clients.Interfaces
{
    public interface ITvShowClient
    {
        Task<IEnumerable<string>> GetCurrentTvShowsAsync();
        Task<bool> AddTvShowAsync(TCRequest tvShow);
    }
}
