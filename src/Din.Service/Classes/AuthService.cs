using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Din.Data;
using Din.ExternalModels.Entities;
using Din.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Din.Service.Classes
{
    /// <inheritdoc />
    public class AuthService : IAuthService
    {
        private readonly DinContext _context;

        public AuthService(DinContext context) 
        {
            _context = context;
        }

        public async Task<Tuple<User, ClaimsPrincipal>> LoginAsync(string username, string password)
        {
            var user = await _context.User.Include(u => u.Account).FirstOrDefaultAsync(u => u.Account.Username.Equals(username));
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Account.Hash)) return null;
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, user.Account.Role.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName)
            };
            return new Tuple<User, ClaimsPrincipal>(user, new ClaimsPrincipal(new ClaimsIdentity(claims, "login")));
        }
    }
}