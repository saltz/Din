using System.Collections.Generic;
using System.Security.Claims;
using Din.Controllers;
using Din.Data.Entities;
using Din.Service.DTO;
using Din.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Din.Tests.Controllers
{
    public class MainControllerTest
    {
        [Fact]
        public void IndexUnAuthenticatedTest()
        {
            var mockService = new Mock<IMediaService>();

            var controller = new MainController(mockService.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext()
                }
            };

            var result = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result.Result);
            Assert.Equal("Index", viewResult.ViewName);
        }

        [Fact]
        public void IndexAuthenticatedTest()
        {
            var mockService = new Mock<IMediaService>();
            mockService.Setup(service => service.GenerateBackgroundImages()).ReturnsAsync(new MediaDTO());

            var controller = new MainController(mockService.Object)
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

            var viewResult = Assert.IsType<ViewResult>(result.Result);
            Assert.Equal("Home", viewResult.ViewName);
        }
    }
}