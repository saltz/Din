using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DinWebsite.Database;
using DinWebsite.ExternalModels.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DinWebsite.Controllers
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