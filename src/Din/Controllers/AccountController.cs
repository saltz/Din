using Din.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Din.Controllers
{
    public class AccountController : Controller
    {
        private DinContext _context;

        public AccountController(DinContext context)
        {
            _context = context;
        }

        [Authorize]
        public void CreateUser()
        {
        }

        [Authorize]
        public void GetUser()
        {

        }
    }
}