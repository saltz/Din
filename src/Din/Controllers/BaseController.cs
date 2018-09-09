using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Din.Controllers
{
    public class BaseController : Controller
    {
        #region methods

        protected int GetCurrentSessionId()
        {
            return Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type.Equals("ID")).Value);
        }

        protected string GetClientUaString()
        {
            return HttpContext.Request.Headers["User-Agent"].ToString();
        }

        protected string GetClientIp()
        {
            return Request.Headers["X-Real-IP"].ToString();
        }

        #endregion methods
    }
}