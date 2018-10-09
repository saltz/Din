using Din.Service.Generators.Interfaces;
using Moq;

namespace Din.Tests.Fixtures
{
    public class MainFixture : BaseFixture
    {
        public Mock<IMediaGenerator> MockService { get; }

        public MainFixture()
        {
            MockService = new Mock<IMediaGenerator>();
        }
    }
}
