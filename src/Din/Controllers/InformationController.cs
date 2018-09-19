using System;
using System.Linq;
using System.Threading.Tasks;
using Din.Service.DTO.Content;
using Din.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Din.Controllers
{
    public class InformationController : BaseController
    {
        #region injections

        private readonly IMovieService _movieService;
        private readonly ITvShowService _tvShowService;

        #endregion injections

        #region constructors

        public InformationController(IMovieService movieService, ITvShowService tvShowService)
        {
            _movieService = movieService;
            _tvShowService = tvShowService;
        }

        #endregion constructors

        #region endpoints

        [Authorize, HttpGet]
        public async Task<IActionResult> GetReleaseCalendarAsync()
        {
            var calendarDto = new CalendarDto
            {
                Items = (await _movieService.GetMovieCalendarAsync()).Concat(
                    await _tvShowService.GetTvShowCalendarAsync()),
                DateRange = new Tuple<DateTime, DateTime>(DateTime.Now, DateTime.Now.AddMonths(1))
            };

            return Ok(calendarDto);
        }

        #endregion endpoints
    }
}