using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Din.Service.Services.Interfaces;
using Din.ViewModels;

namespace Din.Controllers
{
    public class StatusCodeController : BaseController
    {
        #region injections

        private readonly ILogger<MainController> _logger;
        private readonly IStatusCodeService _service;

        #endregion injections

        #region constructors

        public StatusCodeController(ILogger<MainController> logger, IStatusCodeService service)
        {
            _logger = logger;
            _service = service;
        }

        #endregion constructors

        #region endpoints

        // GET: /<controller>/
        [HttpGet("/StatusCode/{statusCode}"), AllowAnonymous]
        public async Task<IActionResult> Index(int statusCode)
        {
            var reExecute = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            _logger.LogInformation($"Unexpected Status Code: {statusCode}, OriginalPath: {reExecute.OriginalPath}");

            return View(new StatusCodeViewModel {Data = await _service.GenerateDataToDisplayAsync(statusCode)});
        }

        #endregion endpoints
    }
}