using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Din.Service.Clients.Interfaces
{
    public interface IMovieClient
    {
        Task<IEnumerable<int>> GetCurrentMoviesAsync();
        Task<bool> AddMovieAsync();
    }
}
