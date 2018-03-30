using System;
using System.Linq;
using System.Net;
using DinWebsite.Database;
using DinWebsite.ExternalModels.Authentication;
using DinWebsite.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DinWebsite.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly DinWebsiteContext _context;

        public AuthenticationController(DinWebsiteContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
            _context.SaveChanges();
        }

        [HttpPost]
        public ActionResult Login()
        {
            try
            {
                var username = Request.Form["username"];
                var auth = _context.Authentication.First(u => u.Username == username);
                if (BCrypt.Net.BCrypt.Verify(Request.Form["password"], auth.Hash))
                {
                    var contentManager = new ContentManager();
                    HttpContext.Session.SetString("BackgroundImages", contentManager.GenerateBackground());
                    var user = _context.Users.First(u => u.ID == auth.UserRef);
                    HttpContext.Session.SetString("User",);
                    return View("../Main/Home");
                }
                else
                {
                    HttpContext.Session.SetString("Login", "BAD");
                    return RedirectToAction("Index", "Main");
                }
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("Login", "BAD");
                return RedirectToAction("Index", "Main");
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Main");
        }
    }
}