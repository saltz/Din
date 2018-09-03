using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Din.Controllers
{
    public class MainController : BaseController
    {
        #region endpoints

        [HttpGet, AllowAnonymous]
        public IActionResult Index()
        {
            return HttpContext.User.Identity.AuthenticationType != null ? View("Home") : View("Index");
        }

        #endregion endpoints
    }
}