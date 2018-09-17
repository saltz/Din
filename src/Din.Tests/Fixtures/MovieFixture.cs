using AutoMapper;
using Din.Mapping.Profiles;
using Din.Service.Services.Interfaces;
using Moq;

namespace Din.Tests.Fixtures
{
    public class MovieFixture : BaseFixture
    {
        public Mock<IMovieService> MockService { get; set; }
        public IMapper Mapper { get; }

        public MovieFixture()
        {
            MockService = new Mock<IMovieService>();
            Mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ViewModelProfile());
            }));
        }
    }
}
