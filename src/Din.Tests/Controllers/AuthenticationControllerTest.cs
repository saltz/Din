using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Din.Controllers;
using Din.Data.Entities;
using Din.Service.Services.Interfaces;
using Din.Tests.Fixtures;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
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
                _.LogLoginAttempt(TestConsts.Username, TestConsts.UserAgent, TestConsts.PublicIp, LoginStatus.Success));

            var controller = new AuthenticationController(_fixture.MockService.Object)
            {
                ControllerContext = _fixture.AuthControllerContext()
            };

            controller.ControllerContext.HttpContext.Request.Headers["User-Agent"] = TestConsts.UserAgent;
            controller.ControllerContext.HttpContext.Request.Headers["X-Real-IP"] = TestConsts.PublicIp;

            var result = controller.LoginAsync(TestConsts.Username,TestConsts.Password).Result;
            Assert.IsType<RedirectToActionResult>(result);
        }
    }
}