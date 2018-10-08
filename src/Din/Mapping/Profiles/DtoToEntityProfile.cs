using AutoMapper;
using Din.Data.Entities;
using Din.Service.Dto.Context;

namespace Din.Mapping.Profiles
{
    public class DtoToEntityProfile : Profile
    {
        public DtoToEntityProfile()
        {
            CreateMap<LoginAttemptDto, LoginAttemptEntity>();
            CreateMap<LoginLocationDto, LoginLocationEntity>();
        }
    }
}