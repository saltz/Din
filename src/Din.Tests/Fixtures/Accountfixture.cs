using Din.Service.Services.Interfaces;
using Moq;

namespace Din.Tests.Fixtures
{
    public class AccountFixture : BaseFixture
    {
        public Mock<IAccountService> AccountServiceMock { get; set; }

        public AccountFixture()
        {
            AccountServiceMock = new Mock<IAccountService>();
        }
    }
}
