using System.Threading.Tasks;
using Din.Data.Entities;
using Din.Service.Services.Interfaces;
using Din.Service.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Din.Controllers
{
    public class AuthenticationController : BaseController
    {
        #region injections

        private readonly IAuthService _service;

        #endregion injections

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
            try
            {
                var loginResult = await _service.LoginAsync(username, password);

                await HttpContext.SignInAsync(loginResult);

                await _service.LogLoginAttempt(username, GetClientUaString(), GetClientIp(), LoginStatus.Success);
                return RedirectToAction("Index", "Main");
            }
            catch (LoginException e)
            {
                await _service.LogLoginAttempt(username, GetClientUaString(), GetClientIp(), LoginStatus.Failed);
                return BadRequest(new {item = e.Identifier, message = e.Message});
            }
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("Exit", "Main");
        }

        #endregion endpoints
    }
}