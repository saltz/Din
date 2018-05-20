using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Din.ExternalModels.Entities;

namespace Din.Service.Interfaces
{
    public interface IAuthService
    {
        Task<Tuple<User, ClaimsPrincipal>> LoginAsync(string username, string password);
    }
}
