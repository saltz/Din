using AutoMapper;
using Din.Service.Dto.Content;
using Din.ViewModels;

namespace Din.Mapping.Profiles
{
    public class ViewModelProfile : Profile
    {
        public ViewModelProfile()
        {
            CreateMap<MovieResultsViewModel, MovieDto>();
            CreateMap<TvShowResultsViewModel, TvShowDto>();
        }
    }
}
