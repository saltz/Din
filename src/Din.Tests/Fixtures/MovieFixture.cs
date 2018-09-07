using AutoMapper;
using Din.Service.DTO;
using Din.Service.DTO.Content;
using Din.Service.Mappers.Concrete;
using Din.Service.Mappers.Interfaces;
using Din.Service.Services.Interfaces;
using Din.ViewModels;
using Moq;

namespace Din.Tests.Fixtures
{
    public class MovieFixture : BaseFixture
    {
        public Mock<IMovieService> MockService { get; set; }
        public IViewModelMapper Mapper { get; }

        public MovieFixture()
        {
            MockService = new Mock<IMovieService>();
            Mapper = new ViewModelMapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MovieResultsViewModel, MovieDTO>();
                cfg.CreateMap<TvShowResultsViewModel, TvShowDTO>();
                cfg.CreateMap<ResultViewModel, ResultDTO>();
            }));
        }
    }
}
