using System.Collections.Generic;
using System.Threading.Tasks;
using Din.Service.Clients.RequestObjects;
using Din.Service.Clients.ResponseObjects;

namespace Din.Service.Clients.Interfaces
{
    public interface ITvShowClient
    {
        Task<IEnumerable<string>> GetCurrentTvShowsAsync();
        Task<bool> AddTvShowAsync(TcRequest tvShow);
        Task<IEnumerable<TcCalendarResponse>> GetCalendarAsync();
    }
}
