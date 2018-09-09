using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Din.Controllers
{
    public class AdminController : BaseController
    {
        #region injections

        #endregion injections

        #region constructors

        #endregion constructors

        #region endpoints

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        #endregion endpoints
    }
}