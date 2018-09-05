using System.Threading.Tasks;
using Din.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TMDbLib.Objects.Search;

namespace Din.Controllers
{
    public class ContentController : BaseController
    {
        #region fields

        private readonly IContentService _service;

        #endregion fields

        #region constructors

        public ContentController(IContentService service)
        {
            _service = service;
        }

        #endregion constructors

        #region endpoints

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
                return PartialView("~/Views/Main/Partials/_Result.cshtml",
                    await _service.AddMovieAsync(movie, GetCurrentSessionId()));
            }
            catch
            {
                return RedirectToAction("Index", "StatusCode", 500);
            }
        }


        [HttpPost, Authorize]
        public async Task<IActionResult> AddTvShowAsync(string tvShowData)
        {
            try
            {
                var tvShow = JsonConvert.DeserializeObject<SearchTv>(tvShowData);
                if (tvShow == null) return RedirectToAction("Index", "StatusCode", 500);
                return PartialView("~/Views/main/Partials/_Result.cshtml",
                    await _service.AddTvShowAsync(tvShow, GetCurrentSessionId()));
            }
            catch
            {
                return RedirectToAction("Index", "StatusCode", 500);
            }
        }

        #endregion endpoints
    }
}