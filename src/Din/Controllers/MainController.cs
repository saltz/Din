using System.Threading.Tasks;
using Din.Service.Clients.Concrete;
using Din.Service.Services.Interfaces;
using Din.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Din.Controllers
{
    public class MainController : BaseController
    {
        private readonly IMediaService _service;

        public MainController(IMediaService service)
        {
            _service = service;
        }

        #region endpoints

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            if (HttpContext.User.Identity.AuthenticationType == null) return View("Index");
            var model = new MediaViewModel {Media = await _service.GenerateBackgroundImages()};
            return View("Home", model);
        }

        public async Task<IActionResult> Exit()
        {
            return View("Logout", new MediaViewModel {Media = await _service.GenerateGif(GiphyTag.Bye)});
        }

        #endregion endpoints
    }
}