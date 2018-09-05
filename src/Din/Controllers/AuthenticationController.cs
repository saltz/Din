using System;
using System.Net.Http;
using System.Threading.Tasks;
using Din.ExternalModels.Entities;
using Din.ExternalModels.Utils;
using Din.Service.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Din.Controllers
{
    public class AuthenticationController : BaseController
    {
        #region fields

        private readonly IAuthService _service;

        #endregion fields

        #region constructors

        public AuthenticationController(IAuthService service)
        {
            _service = service;
        }

        #endregion constructors

        #region endpoints

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> LoginAsync(string username, string password)
        {
            var userAgentString = Request.Headers["User-Agent"].ToString();
            var publicIp = Request.Headers["X-Real-IP"].ToString();
            try
            {
                var loginResult = await _service.LoginAsync(username, password);
                if (loginResult == null)
                    throw new LoginException("Credentials Incorrect");
                await HttpContext.SignInAsync(loginResult);
                await _service.LogLoginAttempt(username, userAgentString, publicIp, LoginStatus.Success);
                return View("~/Views/Main/Home.cshtml");
            }
            catch (LoginException e)
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

        #endregion endpoints
    }
}