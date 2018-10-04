using System.Threading.Tasks;
using Din.Service.Clients.Concrete;
using Din.Service.Generators.Interfaces;
using Din.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Din.Controllers
{
    public class MainController : BaseController
    {
        #region injections

        private readonly IMediaGenerator _generator;

        #endregion injections

        #region constructors

        public MainController(IMediaGenerator generator)
        {
            _generator = generator;
        }

        #endregion constructors

        #region endpoints

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            if (HttpContext.User.Identity.AuthenticationType == null) return View("Index");
            var model = new MediaViewModel {Media = await _generator.GenerateBackgroundImages()};
            return View("Home", model);
        }

        public async Task<IActionResult> Exit()
        {
            return View("Logout", new MediaViewModel {Media = await _generator.GenerateGif(GiphyTag.Bye)});
        }

        #endregion endpoints
    }
}