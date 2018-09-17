using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Din.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
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

        public ControllerContext MockAuthenticationContext()
        {
            var msAuthServiceMock = new Mock<IAuthenticationService>();
            msAuthServiceMock
                .Setup(_ => _.SignInAsync(It.IsAny<HttpContext>(), It.IsAny<string>(), It.IsAny<ClaimsPrincipal>(),
                    It.IsAny<AuthenticationProperties>())).Returns(Task.FromResult((object) null));

            var serviceProviderMock = new Mock<IServiceProvider>();
            var urlHelperFactory = new Mock<IUrlHelperFactory>();

            serviceProviderMock.Setup(_ => _.GetService(typeof(IAuthenticationService)))
                .Returns(msAuthServiceMock.Object);
            serviceProviderMock.Setup(s => s.GetService(typeof(IUrlHelperFactory)))
                .Returns(urlHelperFactory.Object);

            return new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    RequestServices = serviceProviderMock.Object
                }
            };
        }
    }
}