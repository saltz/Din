using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Din.ExternalModels.Entities;

namespace Din.Service
{
    public interface IAuthService
    {
        Task<Tuple<User, ClaimsPrincipal>> LoginAsync(string username, string password);
    }
}
