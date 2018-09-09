using AutoMapper;
using Din.Service.DTO.Content;
using Din.ViewModels;

namespace Din.MapperProfiles
{
    public class ViewModelProfile : Profile
    {
        public ViewModelProfile()
        {
            CreateMap<MovieResultsViewModel, MovieDTO>();
            CreateMap<TvShowResultsViewModel, TvShowDTO>();
        }
    }
}
