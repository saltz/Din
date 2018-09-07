using System;
using System.Threading.Tasks;
using AutoMapper;
using Din.Service.Services.Interfaces;
using Din.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;using UAParser;


namespace Din.Controllers
{
    public class AccountController : BaseController
    {
        #region injections

        private readonly IAccountService _service;
        private readonly IMapper _mapper;

        #endregion injections

        #region constructors

        public AccountController(IAccountService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        #endregion constructors

        #region endpoints

        [Authorize, HttpGet]
        public async Task<IActionResult> GetUserViewAsync()
        {
            var accountDataViewModel = new AccountViewModel
            {
                Data = await _service.GetAccountDataAsync(GetCurrentSessionId()),
                ClientInfo = Parser.GetDefault().Parse(GetClientUaString())
            };

            return PartialView("~/Views/Account/_Account.cshtml", accountDataViewModel);
        }

        [Authorize, HttpPost]
        public async Task<IActionResult> UploadAccountImageAsync(IFormFile file)
        {
            throw new NotImplementedException();
        }

        [Authorize, HttpGet]
        public async Task<IActionResult> GetMovieCalendarAsync()
        {
            throw new NotImplementedException();
        }

        [Authorize, HttpGet]
        public async Task<IActionResult> GetTvShowCalendarAsync()
        {
            throw new NotImplementedException();
        }

        #endregion endpoints
    }
}
