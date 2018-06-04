using System;
using System.Threading.Tasks;
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

        public AuthenticationController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> LoginAsync(string username, string password)
        {
            try
            {
                var loginResult = await _service.LoginAsync(username, password);
                if (loginResult == null)
                    return BadRequest();
                await HttpContext.SignInAsync(loginResult.Item2);
                var serializedUser = JsonConvert.SerializeObject(loginResult.Item1, Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                HttpContext.Session.SetString("UserAgent", Request.Headers["User-Agent"].ToString());
                HttpContext.Session.SetString("User", serializedUser);
                return View("~/Views/Main/Home.cshtml");
            }
            catch (Exception)
            {
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