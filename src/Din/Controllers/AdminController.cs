using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Din.Controllers
{
    public class AdminController : BaseController
    {
        #region fields

        #endregion fields

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