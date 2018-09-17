using Din.Service.Services.Interfaces;
using Moq;

namespace Din.Tests.Fixtures
{
    public class AccountFixture : BaseFixture
    {
        public Mock<IAccountService> AccountServiceMock { get; set; }
        public Mock<IMovieService> MovieServiceMock { get; set; }
        public Mock<ITvShowService> TvShowServiceMock { get; set; }

        public AccountFixture()
        {
            AccountServiceMock = new Mock<IAccountService>();
            MovieServiceMock = new Mock<IMovieService>();
            TvShowServiceMock = new Mock<ITvShowService>();
        }
    }
}
