using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Din.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Din.Controllers
{
    public class ContentController : Controller
    {
        public IActionResult SearchMovie()
        {
            var searchQuery = Request.Form["searchQuery"];
            if (string.IsNullOrEmpty(searchQuery)) return RedirectToAction("Index", "Main");
            var contentManager = new ContentManager();
            HttpContext.Session.SetString("searchResult", JsonConvert.SerializeObject(contentManager.TmdbSearchMovie(searchQuery)));
            HttpContext.Session.SetString("currentMovies", JsonConvert.SerializeObject(contentManager.MediaSystemGetCurrentMovies()));
            return View("../Content/MovieResults");
        }

        public IActionResult SearchTvShow()
        {
            return null;
        }

        public IActionResult AddMovie()
        {
            return null;
        }

        public IActionResult AddTvShow()
        {
            return null;
        }
    }
}