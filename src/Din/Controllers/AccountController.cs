using Din.Data;
using Microsoft.AspNetCore.Mvc;

namespace Din.Controllers
{
    public class AccountController : Controller
    {
        private DinWebsiteContext _context;

        public AccountController(DinWebsiteContext context)
        {
            _context = context;
        }
        public void CreateUser()
        {

        }

        public void GetUser()
        {

        }
    }
}