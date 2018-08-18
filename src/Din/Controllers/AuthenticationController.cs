using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Din.ExternalModels.Entities;
using Din.ExternalModels.Utils;
using Din.Service.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Din.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAuthService _service;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticationController(IAuthService service, IHttpContextAccessor httpContextAccessor)
        {
            _service = service;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> LoginAsync(string username, string password)
        {
            var userAgentString = Request.Headers["User-Agent"].ToString();
            var publicIp = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            try
            {
                var loginResult = await _service.LoginAsync(username, password);
                if (loginResult == null)
                    throw new LoginException("Credentials Incorrect");
                await HttpContext.SignInAsync(loginResult.Item2);
                var serializedUser = JsonConvert.SerializeObject(loginResult.Item1, Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                await _service.LogLoginAttempt(username, userAgentString, publicIp, LoginStatus.Success);
                HttpContext.Session.SetString("UserAgent", userAgentString);
                HttpContext.Session.SetString("User", serializedUser);
                return View("~/Views/Main/Home.cshtml");
            }
            catch (Exception)
            {
                await _service.LogLoginAttempt(username, userAgentString, publicIp, LoginStatus.Failed);
                return BadRequest();
            }
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.Clear();
            return View("~/Views/Main/Logout.cshtml");
        }
    }
}