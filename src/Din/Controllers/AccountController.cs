using Din.Data;
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
        public void CreateUser()
        {
        }

        public void GetUser()
        {

        }
    }
}