using System.Threading.Tasks;
using Din.Service.Mappers.Interfaces;
using Din.Service.Services.Interfaces;
using Din.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TMDbLib.Objects.Search;

namespace Din.Controllers
{
    public class MovieController : BaseController
    {
        private readonly IMovieService _service;
        private readonly IViewModelMapper _mapper;

        public MovieController(IMovieService service, IViewModelMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> SearchMovieAsync(string query)
        {
            if (string.IsNullOrEmpty(query)) return BadRequest();

            return PartialView("~/Views/Main/Partials/_MovieResults.cshtml",
                      _mapper.Instance.Map<MovieResultsViewModel>(await _service.SearchMovieAsync(query)));
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> AddMovieAsync(string movieData)
        {
            try
            {
                var movie = JsonConvert.DeserializeObject<SearchMovie>(movieData);

                if (movie == null) return RedirectToAction("Index", "StatusCode", 500);

                return PartialView("~/Views/Main/Partials/_Result.cshtml",
                   _mapper.Instance.Map<ResultViewModel>(await _service.AddMovieAsync(movie, GetCurrentSessionId())));
            }
            catch
            {
                return RedirectToAction("Index", "StatusCode", 500);
            }
        }
    }
}