using System.Threading.Tasks;
using AutoMapper;
using Din.Service.DTO.Content;
using Din.Service.Services.Interfaces;
using Din.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StackExchange.Redis;
using TMDbLib.Objects.Search;

namespace Din.Controllers
{
    public class MovieController : BaseController
    {
        private readonly IMovieService _service;

        public MovieController(IMovieService service)
        {
            _service = service;
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> SearchMovieAsync(string query)
        {
            if (string.IsNullOrEmpty(query)) return RedirectToAction("Index", "Main");

            return PartialView("~/Views/Main/Partials/_MovieResults.cshtml",
                      Mapper.Map<MovieResultsViewModel>(await _service.SearchMovieAsync(query)));
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> AddMovieAsync(string movieData)
        {
            try
            {
                var movie = JsonConvert.DeserializeObject<SearchMovie>(movieData);

                if (movie == null) return RedirectToAction("Index", "StatusCode", 500);



                return PartialView("~/Views/Main/Partials/_Result.cshtml",
                    Mapper.Map<ResultViewModel>(await _service.AddMovieAsync(movie, GetCurrentSessionId())));
            }
            catch
            {
                return RedirectToAction("Index", "StatusCode", 500);
            }
        }
    }
}