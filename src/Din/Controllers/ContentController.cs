using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Din.ExternalModels.Entities;
using Din.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators.Internal;
using Newtonsoft.Json;
using TMDbLib.Objects.Search;

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
            var input = Request.Form["selected-movie"];
            if (!string.IsNullOrEmpty(input))
            {
                var movieId = Convert.ToInt32(input);
                foreach (var m in JsonConvert.DeserializeObject<List<SearchMovie>>(HttpContext.Session.GetString("searchResults")))
                {
                    if (m.Id.Equals(movieId)) continue;
                    var contentManager = new ContentManager();
                    if (contentManager.MediaSystemAddMovie(m,
                        JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("User")).Account))
                    {
                        var result = new Dictionary<string, string>
                        {
                            {"title", "Movie Added Succesfully"},
                            {
                                "message",
                                "The Movie has been added 🤩. You can track the progress under your account profile tab."
                            }
                        };
                        HttpContext.Session.SetString("contentAdded", JsonConvert.SerializeObject(result));
                        return View("../Main/Home");
                    }
                    else
                    {
                        var result = new Dictionary<string, string>
                        {
                            {"title", "Failed At adding Movie"},
                            {
                                "message",
                                "Somethning went wrong 😵. Try again later!"
                            }
                        };
                        HttpContext.Session.SetString("contentAdded", JsonConvert.SerializeObject(result));
                        return View("../Main/Home");
                    }
                }
            }
            return RedirectToAction("Index", "StatusCode", 500);
        }

        public IActionResult AddTvShow()
        {
            return null;
        }
    }
}