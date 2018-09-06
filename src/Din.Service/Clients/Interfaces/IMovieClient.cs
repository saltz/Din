using System.Collections.Generic;
using System.Threading.Tasks;
using Din.Service.Clients.RequestObjects;

namespace Din.Service.Clients.Interfaces
{
    public interface IMovieClient
    {
        Task<IEnumerable<int>> GetCurrentMoviesAsync();
        Task<bool> AddMovieAsync(MCRequest movie);
    }
}
