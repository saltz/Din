using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Din.Service.Clients.Interfaces
{
    public interface ITvShowClient
    {
        Task<IEnumerable<int>> GetCurrentTvShowsAsync();
        Task<bool> AddTvShowAsync();
    }
}
