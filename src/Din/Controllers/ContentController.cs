using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Din.Data;
using Din.ExternalModels.Entities;
using Din.Logic;
using Din.Logic.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TMDbLib.Objects.Search;

namespace Din.Controllers
{
    public class ContentController : Controller
    {
        private readonly DinContext _context;

        public ContentController(DinContext context)
        {
            _context = context;
        }


        [HttpPost, Authorize]
        public async Task<IActionResult> SearchMovieAsync(string query)
        {
            if (string.IsNullOrEmpty(query)) return RedirectToAction("Index", "Main");
            var contentManager = new ContentManager();
            HttpContext.Session.SetString("searchResults",
                JsonConvert.SerializeObject(await contentManager.TmdbSearchMovieAsync(query)));
            HttpContext.Session.SetString("currentMovies",
                JsonConvert.SerializeObject(await contentManager.MediaSystemGetCurrentMoviesAsync()));
            return PartialView("~/Views/Main/Partials/_SearchResults.cshtml");
        }


        [HttpPost, Authorize]
        public async Task<IActionResult> SearchTvShowAsync()
        {
            return null;
        }


        [HttpPost, Authorize]
        public async Task<IActionResult> AddMovieAsync(string id)
        {
            if (string.IsNullOrEmpty(id)) return RedirectToAction("Index", "StatusCode", 500);
            var movieId = Convert.ToInt32(id);
            foreach (var m in JsonConvert.DeserializeObject<List<SearchMovie>>(
                HttpContext.Session.GetString("searchResults")))
            {
                if (!m.Id.Equals(movieId)) continue;
                var contentManager = new ContentManager(_context);
                if (await contentManager.MediaSystemAddMovie(m,
                    JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("User")).Account))
                {
                    var result = new Dictionary<string, string>
                    {
                        {"color", "#00d77c;"},
                        {"title", "Movie Added Succesfully"},
                        {
                            "message",
                            "The Movie has been added 🤩   You can track the progress under your account profile tab."
                        }
                    };
                    HttpContext.Session.SetString("contentAdded", JsonConvert.SerializeObject(result));
                }
                else
                {
                    var result = new Dictionary<string, string>
                    {
                        {"color", "#b43232"},
                        {"title", "Failed At adding Movie"},
                        {
                            "message",
                            "Somethning went wrong 😵   Try again later!"
                        }
                    };
                    HttpContext.Session.SetString("contentAdded", JsonConvert.SerializeObject(result));
                }
                return PartialView("~/Views/Main/Partials/_AddResult.cshtml");
            }
            return RedirectToAction("Index", "StatusCode", 500);
        }


        [HttpPost, Authorize]
        public async Task<IActionResult> AddTvShowAsync()
        {
            return null;
        }
    }
}