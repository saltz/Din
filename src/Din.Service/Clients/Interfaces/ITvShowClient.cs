using System.Collections.Generic;
using System.Threading.Tasks;
using Din.Service.Clients.RequestObjects;
using Din.Service.Clients.ResponseObjects;

namespace Din.Service.Clients.Interfaces
{
    public interface ITvShowClient
    {
        Task<IEnumerable<TcTvShowResponse>> GetCurrentTvShowsAsync();
        Task<TcTvShowResponse> GetTvShowByIdAsync(int id);
        Task<(bool status, int systemId)> AddTvShowAsync(TcRequest tvShow);
        Task<IEnumerable<TcCalendarResponse>> GetCalendarAsync();
    }
}
