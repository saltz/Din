using System;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
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
        #region fields

        private readonly IAccountService _service;

        #endregion fields

        #region constructors

        public AccountController(IAccountService service)
        {
            _service = service;
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
            var ms = new MemoryStream();
            await file.OpenReadStream().CopyToAsync(ms);

            return PartialView("~/Views/Main/Partials/_Result.cshtml", Mapper.Map<ResultViewModel>(
                await _service.UploadAccountImageAsync(GetCurrentSessionId(), file.Name, ms.ToArray())));
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
