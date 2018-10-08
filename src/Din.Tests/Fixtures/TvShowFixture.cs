using AutoMapper;
using Din.Mapping.Profiles;
using Din.Service.Services.Interfaces;
using Moq;

namespace Din.Tests.Fixtures
{
    public class TvShowFixture : BaseFixture
    {
        public Mock<ITvShowService> MockService { get; set; }
        public IMapper Mapper { get; }

        public TvShowFixture()
        {
            MockService = new Mock<ITvShowService>();
            Mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ViewModelToDtoProfile());
            }));
        }
    }
}
