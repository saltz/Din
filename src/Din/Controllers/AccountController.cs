using System.Threading.Tasks;
using Din.Data;
using Din.ExternalModels.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Din.Controllers
{
    public class AccountController : Controller
    {
        private readonly DinContext _context;

        public AccountController(DinContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task CreateUser(User user)
        {
            await _context.User.AddAsync(user);
            _context.SaveChanges();
        }

        [Authorize]
        public void GetUser()
        {
        }
    }
}