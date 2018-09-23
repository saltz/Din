using System;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Din.Service.Dto.Context;
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
        public async Task<IActionResult> UploadAccountImageAsync(IFormFile image)
        {
            if (image != null)
            {
                byte[] data;
                using (var rs = image.OpenReadStream())
                using (var ms = new MemoryStream())
                {
                    rs.CopyTo(ms);
                    data = ms.ToArray();
                }

                return PartialView("~/Views/Main/Partials/_Result.cshtml", _mapper.Map<ResultViewModel>(await _service.UploadAccountImageAsync(GetCurrentSessionId(), image.FileName, data)));
            }

            return BadRequest();
        }

        [Authorize, HttpPost]
        public async Task<IActionResult> UpdatePersonalInformation(string firstname, string lastname)
        {
            return PartialView("~/Views/Main/Partials/_Result.cshtml", _mapper.Map<ResultViewModel>(
                await _service.UpdatePersonalInformation(GetCurrentSessionId(), new UserDto
                {
                    FirstName = firstname,
                    LastName = lastname
                })));
        }

        [Authorize, HttpPost]
        public async Task<IActionResult> UpdateAccountInformation(string accountUsername, string validPassword)
        {
            var hash = validPassword != null ? BCrypt.Net.BCrypt.HashPassword(validPassword) : null;

            return PartialView("~/Views/Main/Partials/_Result.cshtml", _mapper.Map<ResultViewModel>(
                await _service.UpdateAccountInformation(GetCurrentSessionId(), accountUsername, hash)));
        }

        #endregion endpoints
    }
}