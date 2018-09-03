using System.IO;
using System.Threading.Tasks;
using Din.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            return PartialView("~/Views/Account/_Account.cshtml",
                await _service.GetAccountDataAsync(GetCurrentSessionId(), GetCurrentUaString()));
        }

        [Authorize, HttpPost]
        public async Task<IActionResult> UploadAccountImageAsync(IFormFile file)
        {
            var ms = new MemoryStream();
            await file.OpenReadStream().CopyToAsync(ms);
            return PartialView("~/Views/Main/Partials/_Result.cshtml",
                await _service.UploadAccountImageAsync(GetCurrentSessionId(), file.Name, ms.ToArray()));
        }

        #endregion endpoints
    }
}