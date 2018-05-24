using System.Threading.Tasks;
using Din.ExternalModels.Entities;
using Din.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TMDbLib.Objects.Search;

namespace Din.Controllers
{
    public class ContentController : Controller
    {
        private readonly IContentService _service;

        public ContentController(IContentService service)
        {
            _service = service;
        }


        [HttpPost, Authorize]
        public async Task<IActionResult> SearchMovieAsync(string query)
        {
            if (string.IsNullOrEmpty(query)) return RedirectToAction("Index", "Main");
            return PartialView("~/Views/Main/Partials/_MovieResults.cshtml",
                await _service.SearchMovieAsync(query));
        }


        [HttpPost, Authorize]
        public async Task<IActionResult> SearchTvShowAsync(string query)
        {
            if (string.IsNullOrEmpty(query)) return RedirectToAction("Index", "Main");
            return PartialView("~/Views/Main/Partials/_TvShowResults.cshtml", await _service.SearchTvShowAsync(query));
        }


        [HttpPost, Authorize]
        public async Task<IActionResult> AddMovieAsync(string movieData)
        {
            try
            {
                var movie = JsonConvert.DeserializeObject<SearchMovie>(movieData);
                if (movie == null) return RedirectToAction("Index", "StatusCode", 500);
                return PartialView("~/Views/Main/Partials/_AddResult.cshtml", await _service.AddMovieAsync(movie,
                    JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("User")).Account));
            }
            catch
            {
                return RedirectToAction("Index", "StatusCode", 500);
            }
        }


        [HttpPost, Authorize]
        public async Task<IActionResult> AddTvShowAsync()
        {
            return null;
        }
    }
}