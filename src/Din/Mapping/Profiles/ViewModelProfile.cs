using AutoMapper;
using Din.Service.Dto.Content;
using Din.ViewModels;
using TMDbLib.Objects.Search;

namespace Din.Mapping.Profiles
{
    public class ViewModelProfile : Profile
    {
        public ViewModelProfile()
        {
            CreateMap<SearchResultViewModel<int, SearchMovie>, SearchResultDto<int, SearchMovie>>();
            CreateMap<SearchResultViewModel<string, SearchTv>, SearchResultDto<string, SearchTv>>();
        }
    }
}
