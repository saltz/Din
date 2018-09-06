using System.Threading.Tasks;
using AutoMapper;
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

        public TvShowController(ITvShowService service)
        {
            _service = service;
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> SearchTvShowAsync(string query)
        {
            if (string.IsNullOrEmpty(query)) return RedirectToAction("Index", "Main");

            return PartialView("~/Views/Main/Partials/_TvShowResults.cshtml",
                Mapper.Map<TvShowResultsViewModel>(await _service.SearchTvShowAsync(query)));
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> AddTvShowAsync(string tvShowData)
        {
            try
            {
                var tvShow = JsonConvert.DeserializeObject<SearchTv>(tvShowData);

                if (tvShow == null) return RedirectToAction("Index", "StatusCode", 500);

                return PartialView("~/Views/main/Partials/_Result.cshtml",
                    Mapper.Map<ResultViewModel>(await _service.AddTvShowAsync(tvShow, GetCurrentSessionId())));
            }
            catch
            {
                return RedirectToAction("Index", "StatusCode", 500);
            }
        }
    }
}