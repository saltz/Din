using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Din.Data;
using Din.Data.Entities;
using Din.Service.Clients.Interfaces;
using Din.Service.Dto.Context;
using Din.Service.Services.Interfaces;
using Din.Service.Utils;
using Microsoft.EntityFrameworkCore;
using UAParser;

namespace Din.Service.Services.Concrete
{
    /// <inheritdoc />
    public class AuthService : IAuthService
    {
        private readonly DinContext _context;
        private readonly IIpStackClient _ipStackClient;
        private readonly IMapper _mapper;

        public AuthService(DinContext context, IIpStackClient ipStackClient, IMapper mapper)
        {
            _context = context;
            _ipStackClient = ipStackClient;
            _mapper = mapper;
        }

        public async Task<ClaimsPrincipal> LoginAsync(string username, string password)
        {
            try
            {
                var accountEntity = await _context.Account.FirstAsync(a => a.Username.Equals(username));

                if (!BCrypt.Net.BCrypt.Verify(password, accountEntity.Hash))
                    throw new LoginException("Password Incorrect", 2);

                var claims = new List<Claim>
                {
                    new Claim("ID", accountEntity.Id.ToString()),
                    new Claim(ClaimTypes.Role, accountEntity.Role.ToString())
                };

                return new ClaimsPrincipal(new ClaimsIdentity(claims, "login"));
            }
            catch (InvalidOperationException)
            {
                throw new LoginException("Username incorrect", 1);
            }
        }

        public async Task LogLoginAttempt(string username, string userAgentString, string publicIp, LoginStatus status)
        {
            var clientInfo = Parser.GetDefault().Parse(userAgentString);
            var loginAttemptDto = new LoginAttemptDto
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
                loginAttemptDto.Location = _mapper.Map<LoginLocationDto>(await _ipStackClient.GetLocation(publicIp));

                await _context.LoginAttempt.AddAsync(_mapper.Map<LoginAttemptEntity>(loginAttemptDto));
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                await _context.LoginAttempt.AddAsync(_mapper.Map<LoginAttemptEntity>(loginAttemptDto));
                await _context.SaveChangesAsync();
            }
        }
    }
}