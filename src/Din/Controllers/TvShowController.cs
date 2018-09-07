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
    public class TvShowController : BaseController
    {
        private readonly ITvShowService _service;
        private readonly IViewModelMapper _mapper;

        public TvShowController(ITvShowService service, IViewModelMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> SearchTvShowAsync(string query)
        {
            if (string.IsNullOrEmpty(query)) return RedirectToAction("Index", "Main");

            return PartialView("~/Views/Main/Partials/_TvShowResults.cshtml",
                _mapper.Instance.Map<TvShowResultsViewModel>(await _service.SearchTvShowAsync(query)));
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> AddTvShowAsync(string tvShowData)
        {
            try
            {
                var tvShow = JsonConvert.DeserializeObject<SearchTv>(tvShowData);

                if (tvShow == null) return RedirectToAction("Index", "StatusCode", 500);

                return PartialView("~/Views/main/Partials/_Result.cshtml",
                    _mapper.Instance.Map<ResultViewModel>(await _service.AddTvShowAsync(tvShow, GetCurrentSessionId())));
            }
            catch
            {
                return RedirectToAction("Index", "StatusCode", 500);
            }
        }
    }
}