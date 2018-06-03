using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace Din.Controllers
{
    public class MainController : Controller
    {
        [HttpGet, AllowAnonymous]
        public IActionResult Index()
        {
            return HttpContext.Session.GetString("User") != null ? View("Home") : View();
        }
    }
}
