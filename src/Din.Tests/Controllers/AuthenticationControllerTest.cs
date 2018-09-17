using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Din.Controllers;
using Din.Data.Entities;
using Din.Service.Utils;
using Din.Tests.Fixtures;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Din.Tests.Controllers
{
    public class AuthenticationControllerTest : IClassFixture<AuthFixture>
    {
        private readonly AuthFixture _fixture;

        public AuthenticationControllerTest(AuthFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public void LoginAsyncTest()
        {
            _fixture.MockService.Setup(service => service.LoginAsync(TestConsts.Username, TestConsts.Password))
                .ReturnsAsync(_fixture.Principal);
            _fixture.MockService.Setup(_ =>
                _.LogLoginAttempt(TestConsts.Username, TestConsts.UserAgent, TestConsts.PublicIp, LoginStatus.Success)).Returns(Task.FromResult((object)null));

            var controller = new AuthenticationController(_fixture.MockService.Object)
            {
                ControllerContext = _fixture.MockAuthenticationContext()
            };

            controller.ControllerContext.HttpContext.Request.Headers["User-Agent"] = TestConsts.UserAgent;
            controller.ControllerContext.HttpContext.Request.Headers["X-Real-IP"] = TestConsts.PublicIp;

            var result = controller.LoginAsync(TestConsts.Username,TestConsts.Password).Result;
            var actionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", actionResult.ActionName);
            Assert.Equal("Main", actionResult.ControllerName);
        }

        [Fact]
        public void LoginAsyncFailTest()
        {
            _fixture.MockService.Setup(service => service.LoginAsync(TestConsts.Username, TestConsts.Password)).ThrowsAsync(new LoginException(TestConsts.ExceptionMessage, TestConsts.ExceptionId));
            _fixture.MockService.Setup(_ =>
                _.LogLoginAttempt(TestConsts.Username, TestConsts.UserAgent, TestConsts.PublicIp, LoginStatus.Failed)).Returns(Task.FromResult((object)null));

            var controller = new AuthenticationController(_fixture.MockService.Object)
            {
                ControllerContext = _fixture.MockAuthenticationContext()
            };

            controller.ControllerContext.HttpContext.Request.Headers["User-Agent"] = TestConsts.UserAgent;
            controller.ControllerContext.HttpContext.Request.Headers["X-Real-IP"] = TestConsts.PublicIp;

            var result = controller.LoginAsync(TestConsts.Username, TestConsts.Password).Result;

            var resultObject = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(TestConsts.LoginFailStatusCode, resultObject.StatusCode);
        }
    }
}