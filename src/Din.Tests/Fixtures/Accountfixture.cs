using Din.Service.Services.Interfaces;
using Moq;

namespace Din.Tests.Fixtures
{
    public class AccountFixture : BaseFixture
    {
        public Mock<IAccountService> ServiceMock { get; set; }

        public AccountFixture()
        {
            ServiceMock = new Mock<IAccountService>();
        }
    }
}
