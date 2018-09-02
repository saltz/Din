using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Din.Data;
using Din.ExternalModels.Entities;
using Din.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using UAParser;

namespace Din.Service.Concrete
{
    /// <inheritdoc />
    public class AuthService : IAuthService
    {
        private readonly DinContext _context;

        public AuthService(DinContext context)
        {
            _context = context;
        }

        public async Task<ClaimsPrincipal> LoginAsync(string username, string password)
        {
            var account = await _context.Account.FirstAsync(a => a.Username.Equals(username));
            if (account == null || !BCrypt.Net.BCrypt.Verify(password, account.Hash)) return null;
            var claims = new List<Claim>
            {
                new Claim("ID", account.ID.ToString()),
                new Claim(ClaimTypes.Role, account.Role.ToString())
            };
            return new ClaimsPrincipal(new ClaimsIdentity(claims, "login"));
        }

        public async Task LogLoginAttempt(string username, string userAgentString, string publicIp, LoginStatus status)
        {
            var userAgent = Parser.GetDefault().Parse(userAgentString);
            var geoUrl = MainService.PropertyFile.Get("ipstackBaseUrl") + publicIp +
                         MainService.PropertyFile.Get("ipstackAccessToken");
            try
            {
                var location =
                    JsonConvert.DeserializeObject<LoginLocation>(await new HttpClient().GetStringAsync(geoUrl));
                await _context.LoginAttempt.AddAsync(new LoginAttempt(username, userAgent.Device.Brand,
                    userAgent.OS.Family,
                    userAgent.UA.Family, publicIp, location, DateTime.Now, status));
                await _context.SaveChangesAsync();
            }
            catch (JsonReaderException)
            {
                await _context.LoginAttempt.AddAsync(new LoginAttempt(username, userAgent.Device.Brand,
                    userAgent.OS.Family,
                    userAgent.UA.Family, publicIp, new LoginLocation(), DateTime.Now, status));
                await _context.SaveChangesAsync();
            }
        }
    }
}