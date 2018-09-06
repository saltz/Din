using System.Collections.Generic;
using System.Security.Claims;
using Din.Controllers;
using Din.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Din.Tests.Controllers
{
    public class MainControllerTest
    {
        [Fact]
        public void IndexUnAuthenticatedTest()
        {
            var controller = new MainController
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext()
                }
            };

            var result = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Index", viewResult.ViewName);
        }

        [Fact]
        public void IndexAuthenticatedTest()
        {
            var controller = new MainController
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext()
                }
            };

            var account = new AccountEntity()
            {
                ID = 1,
                Role = AccountRoll.User
            };

            //Fake SignInAsync method from HttpContext
            controller.ControllerContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(
                new List<Claim>
                {
                    new Claim("ID", account.ID.ToString()),
                    new Claim(ClaimTypes.Role, account.Role.ToString())
                }, "login"));

            var result = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Home", viewResult.ViewName);
        }
    }
}