using Din.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Din.Controllers
{
    public class StatusCodeController : BaseController
    {
        #region fields

        private readonly ILogger<MainController> _logger;
        private readonly IStatusCodeService _service;

        #endregion fields

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
            return View(await _service.GenerateDataToDisplayAsync(statusCode));
        }

        #endregion endpoints
    }
}