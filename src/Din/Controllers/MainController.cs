using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Din.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return HttpContext.Session.GetString("User") != null ? View("Home") : View();
        }

        public IActionResult GoBackAndClean()
        {
            HttpContext.Session.Remove("searchResult");
            HttpContext.Session.Remove("currentMovies");
            return RedirectToAction("Index");
        }
    }
}
