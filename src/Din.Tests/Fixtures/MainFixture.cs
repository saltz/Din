using Din.Service.Services.Interfaces;
using Moq;

namespace Din.Tests.Fixtures
{
    public class MainFixture : BaseFixture
    {
        public Mock<IMediaService> MockService { get; }

        public MainFixture()
        {
            MockService = new Mock<IMediaService>();
        }
    }
}
