using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Din.Data;
using Din.ExternalModels.Entities;
using Din.Logic.BrowserDetection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Rest;
using Newtonsoft.Json;

namespace Din.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly DinContext _context;
        private readonly IConfiguration _config;

        public AuthenticationController(DinContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost, AllowAnonymous]
        public ActionResult Login()
        {
            try
            {
                var username = Request.Form["username"];
                var user = _context.User.Include(u => u.Account).FirstOrDefault(u => u.Account.Username.Equals(username));
                if (user != null && BCrypt.Net.BCrypt.Verify(Request.Form["password"], user.Account.Hash))
                {   
                    Authenticate(user);       
                    var serializedUser = JsonConvert.SerializeObject(user, Formatting.Indented,
                        new JsonSerializerSettings
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
                    HttpContext.Session.SetString("UserAgent", Request.Headers["User-Agent"].ToString());
                    HttpContext.Session.SetString("User", serializedUser);
                    return View("../Main/Home");
                }
                else
                {
                    HttpContext.Session.SetString("Login", "BAD");
                    return RedirectToAction("Index", "Main");
                }
            }
            catch (Exception)
            {
                HttpContext.Session.SetString("Login", "BAD");
                return RedirectToAction("Index", "Main");
            }
        }

        [HttpGet, Authorize]
        public ActionResult Logout()
        {
            HttpContext.SignOutAsync();
            HttpContext.Session.Clear();
            return View("../Main/Logout");
        }

        private void Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, user.Account.Role.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName)
            };
            var userIdentity = new ClaimsIdentity(claims, "login");
            var principal = new ClaimsPrincipal(userIdentity);
            HttpContext.SignInAsync(principal);
        }
    }
}