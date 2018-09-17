using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using Din.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Din.Tests.Fixtures
{
    public class AuthFixture : BaseFixture
    {
        public Mock<IAuthService> MockService { get; set; }
        public ClaimsPrincipal Principal { get; set; }

        public AuthFixture()
        {
            MockService = new Mock<IAuthService>();
            Principal = new ClaimsPrincipal(new ClaimsIdentity(
                new List<Claim>
                {
                    new Claim("ID", TestConsts.Id),
                    new Claim(ClaimTypes.Role, TestConsts.Role)
                }, "login"));
        }

        public ControllerContext AuthControllerContext()
        {
            //TODO
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(h => h.SignInAsync(Principal).IsCompletedSuccessfully);
            var controllerContext = new ControllerContext
            {
                HttpContext = httpContext.Object
            };
            return controllerContext;
        }
    }
}