using AutoMapper;
using Din.Data.Entities;
using Din.Service.Dto.Context;

namespace Din.Mapping.Profiles
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile()
        {
            CreateMap<UserEntity, UserDto>();
            CreateMap<AccountEntity, AccountDto>();
            CreateMap<AddedContentEntity, AddedContentDto>();
        }
    }
}
