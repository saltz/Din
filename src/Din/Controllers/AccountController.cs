using System;
using System.Threading.Tasks;
using AutoMapper;
using Din.Service.DTO.Account;
using Din.Service.Services.Interfaces;
using Din.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UAParser;


namespace Din.Controllers
{
    public class AccountController : BaseController
    {
        #region injections

        private readonly IAccountService _accountService;
        private readonly IMovieService _movieService;
        private readonly ITvShowService _tvShowService;
        private readonly IMapper _mapper;

        #endregion injections

        #region constructors

        public AccountController(IAccountService accountService, IMovieService movieService, ITvShowService tvShowService, IMapper mapper)
        {
            _accountService = accountService;
            _movieService = movieService;
            _tvShowService = tvShowService;
            _mapper = mapper;
        }

        #endregion constructors

        #region endpoints

        [Authorize, HttpGet]
        public async Task<IActionResult> GetUserViewAsync()
        {
            var accountDataViewModel = new AccountViewModel
            {
                Data = await _accountService.GetAccountDataAsync(GetCurrentSessionId()),
                ClientInfo = Parser.GetDefault().Parse(GetClientUaString()),
                Calendar = new CalendarDTO
                {
                    MovieCalendar = await _movieService.GetMovieCalendarAsync(),
                    TvShowCalendar = await _tvShowService.GetTvShowCalendarAsync()
                }
            };

            return PartialView("~/Views/Account/_Account.cshtml", accountDataViewModel);
        }

        [Authorize, HttpPost]
        public async Task<IActionResult> UploadAccountImageAsync(IFormFile file)
        {
            throw new NotImplementedException();
        }

        #endregion endpoints
    }
}
