using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Din.Data;
using Din.Data.Entities;
using Din.Service.Clients.Interfaces;
using Din.Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using UAParser;

namespace Din.Service.Services.Concrete
{
    /// <inheritdoc />
    public class AuthService : IAuthService
    {
        private readonly DinContext _context;
        private readonly IIpStackClient _ipStackClient;

        public AuthService(DinContext context, IIpStackClient ipStackClient)
        {
            _context = context;
            _ipStackClient = ipStackClient;
        }

        public async Task<ClaimsPrincipal> LoginAsync(string username, string password)
        {
            var accountEntity = await _context.Account.FirstAsync(a => a.Username.Equals(username));

            if (accountEntity == null || !BCrypt.Net.BCrypt.Verify(password, accountEntity.Hash)) return null;

            var claims = new List<Claim>
            {
                new Claim("ID", accountEntity.ID.ToString()),
                new Claim(ClaimTypes.Role, accountEntity.Role.ToString())
            };

            return new ClaimsPrincipal(new ClaimsIdentity(claims, "login"));
        }

        public async Task LogLoginAttempt(string username, string userAgentString, string publicIp, LoginStatus status)
        {
            var clientInfo = Parser.GetDefault().Parse(userAgentString);
            var loginAttemptEntity = new LoginAttemptEntity
            {
                Username = username,
                Device = clientInfo.Device.Family,
                Os = clientInfo.OS.Family,
                Browser = clientInfo.UA.Family,
                PublicIp = publicIp,
                DateAndTime = DateTime.Now,
                Status = status
            };

            try
            {
                var locationDto = await _ipStackClient.GetLocation(publicIp);

                loginAttemptEntity.Location = Mapper.Map<LoginLocationEntity>(locationDto);

                await _context.LoginAttempt.AddAsync(loginAttemptEntity);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                await _context.LoginAttempt.AddAsync(loginAttemptEntity);
                await _context.SaveChangesAsync();
            }
        }
    }
}