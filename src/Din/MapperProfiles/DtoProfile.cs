using AutoMapper;
using Din.Data.Entities;
using Din.Service.DTO.Context;

namespace Din.MapperProfiles
{
    public class DtoProfile : Profile
    {
        public DtoProfile()
        {
            CreateMap<LoginLocationDTO, LoginLocationEntity>();
        }
    }
}
