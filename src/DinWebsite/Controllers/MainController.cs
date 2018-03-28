using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DinWebsite.Models;
using Microsoft.AspNetCore.Http;

namespace DinWebsite.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login()
        {
            try
            {
                HttpContext.Session.Clear();
                var username = Request.Form["username"];
                var password = Request.Form["password"];

                if (username.Equals("dane"))
                {
                    HttpContext.Session.SetString("login", "ok");
                }

                return RedirectToAction("index");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index");
            }
        }
    }
}
