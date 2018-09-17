using System;
using System.Collections.Generic;
using System.Text;
using Din.Service.Services.Interfaces;
using Moq;

namespace Din.Tests.Fixtures
{
    public class AccountFixture : BaseFixture
    {
        public Mock<IAccountService> MockService { get; set; }

        public AccountFixture()
        {
            MockService = new Mock<IAccountService>();
        }
    }
}
