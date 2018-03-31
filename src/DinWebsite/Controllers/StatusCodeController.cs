using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DinWebsite.ExternalModels.Content;
using DinWebsite.Logic;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DinWebsite.Controllers
{
    public class StatusCodeController : Controller
    {
        private readonly ILogger<MainController> _logger;

        public StatusCodeController(ILogger<MainController> logger)
        {
            _logger = logger;
        }

        // GET: /<controller>/
        [HttpGet("/StatusCode/{statusCode}")]
        public IActionResult Index(int statusCode)
        {
            var reExecute = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            _logger.LogInformation($"Unexpected Status Code: {statusCode}, OriginalPath: {reExecute.OriginalPath}");
            var contentManager = new ContentManager();
            switch (statusCode)
            {
                case 403:
                    HttpContext.Session.SetString("Gif", contentManager.GetRandomGif(GiphyQuery.Forbidden));
                    break;
                case 404:
                    HttpContext.Session.SetString("Gif", contentManager.GetRandomGif(GiphyQuery.PageNotFound));
                    break;
                case 500:
                    HttpContext.Session.SetString("Gif", contentManager.GetRandomGif(GiphyQuery.ServerError));
                    break;
                default:
                    HttpContext.Session.SetString("Gif", contentManager.GetRandomGif(GiphyQuery.Random));
                    break;
            }
            return View(statusCode);
        }
    }
}