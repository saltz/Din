using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Din.Controllers
{
    public class MainController : BaseController
    {
        [HttpGet, AllowAnonymous]
        public IActionResult Index()
        {
            return HttpContext.User.Identity.AuthenticationType != null ? View("Home") : View();
        }
    }
}
