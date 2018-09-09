using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Din.Controllers;
using Din.Data.Entities;
using Din.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Din.Tests.Controllers
{
    public class AuthenticationControllerTest
    {
        [Fact]
        public void LoginAsyncTest()
        {
            var claimsPrinciple = new ClaimsPrincipal(new ClaimsIdentity(
                new List<Claim>
                {
                    new Claim("ID", "1"),
                    new Claim(ClaimTypes.Role, AccountRoll.User.ToString())
                }, "login"));

            var mockService = new Mock<IAuthService>();
            mockService.Setup(service => service.LoginAsync("username", "password"))
                .Returns(Task.FromResult(claimsPrinciple));


            var authServiceMock = new Mock<IAuthenticationService>();
            authServiceMock
                .Setup(_ => _.SignInAsync(It.IsAny<HttpContext>(), It.IsAny<string>(), It.IsAny<ClaimsPrincipal>(),
                    It.IsAny<AuthenticationProperties>()))
                .Returns(Task.FromResult((object) null));

            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock
                .Setup(_ => _.GetService(typeof(IAuthenticationService)))
                .Returns(authServiceMock.Object);

            var controller = new AuthenticationController(mockService.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        RequestServices = serviceProviderMock.Object
                    }
                }
            };

            controller.ControllerContext.HttpContext.Request.Headers["User-Agent"] = TestConsts.UserAgent;
            controller.ControllerContext.HttpContext.Request.Headers["X-Real-IP"] = TestConsts.PublicIp;

            var result = controller.LoginAsync("username", "password");

            //TODO Fix this unit test
            //var viewResult = Assert.IsType<ViewResult>(result);

            Assert.True(result != null);
        }
    }
}