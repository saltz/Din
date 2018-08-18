using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Din.ExternalModels.Entities;

namespace Din.Service.Interfaces
{
    /// <summary>
    /// Authentication service for the corresponsing controller.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// User Login method, authenticates the supplied parameters.
        /// </summary>
        /// <param name="username">Username of the user</param>
        /// <param name="password">Password in hash (BCrypt) format</param>
        /// <returns>
        /// The user object corresponding with supplied credentials and the generated claims principle for authoraztion.
        /// </returns>
        Task<Tuple<User, ClaimsPrincipal>> LoginAsync(string username, string password);

        Task LogLoginAttempt(string username, string userAgentString, string publicIp, LoginStatus status);
    }
}
