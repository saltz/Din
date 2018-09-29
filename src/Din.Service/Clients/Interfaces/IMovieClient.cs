using System.Collections.Generic;
using System.Threading.Tasks;
using Din.Service.Clients.RequestObjects;
using Din.Service.Clients.ResponseObjects;

namespace Din.Service.Clients.Interfaces
{
    public interface IMovieClient
    {
        Task<IEnumerable<McMovieResponse>> GetCurrentMoviesAsync();
        Task<bool> AddMovieAsync(McRequest movie);
        Task<IEnumerable<McCalendarResponse>> GetCalendarAsync();
    }
}
