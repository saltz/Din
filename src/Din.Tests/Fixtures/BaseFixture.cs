using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Din.Tests.Fixtures
{
    public abstract class BaseFixture
    {
        public ControllerContext DefaultControllerContext()
        {
            return new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
        }

        public ControllerContext ControllerContextWithSession()
        {
            return new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                    {
                        new Claim("ID", TestConsts.Id),
                        new Claim(ClaimTypes.Role, TestConsts.Role)
                    }, "login"))
                }
            };
        }
    }
}