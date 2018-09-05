using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Din.Data;
using Din.Data.Entities;
using Din.Service.DTO.ContextDTO;
using Din.Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using UAParser;

namespace Din.Service.Services.Concrete
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
            //TODO fix this
            //var geoUrl = MainService.PropertyFile.Get("ipstackBaseUrl") + publicIp + MainService.PropertyFile.Get("ipstackAccessToken");
            var geoUrl = "";
            try
            {
                var location =
                    JsonConvert.DeserializeObject<LoginLocation>(await new HttpClient().GetStringAsync(geoUrl));
                await _context.LoginAttempt.AddAsync(new LoginAttemptEntity(username, userAgent.Device.Brand,
                    userAgent.OS.Family,
                    userAgent.UA.Family, publicIp, location, DateTime.Now, status));
                await _context.SaveChangesAsync();
            }
            catch (HttpRequestException) //Location Api is down
            {
                await _context.LoginAttempt.AddAsync(new LoginAttemptEntity(username, userAgent.Device.Brand,
                    userAgent.OS.Family, userAgent.UA.Family, publicIp, null, DateTime.Now, status));
                await _context.SaveChangesAsync();
            }
            catch (JsonReaderException) // supplied public ip address results in null
            {
                await _context.LoginAttempt.AddAsync(new LoginAttemptEntity(username, userAgent.Device.Brand,
                    userAgent.OS.Family,
                    userAgent.UA.Family, publicIp, null, DateTime.Now, status));
                await _context.SaveChangesAsync();
            }
        }
    }
}