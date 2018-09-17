using System.Collections.Generic;
using System.Threading.Tasks;
using Din.Service.Clients.RequestObjects;
using Din.Service.Clients.ResponseObjects;

namespace Din.Service.Clients.Interfaces
{
    public interface IMovieClient
    {
        Task<IEnumerable<int>> GetCurrentMoviesAsync();
        Task<bool> AddMovieAsync(MCRequest movie);
        Task<IEnumerable<MCCalendarResponse>> GetCalendarAsync();
    }
}
