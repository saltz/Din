using AutoMapper;
using Din.Data.Entities;
using Din.Service.DTO.Context;

namespace Din.MapperProfiles
{
    public class EntityProfile : Profile
    {
        public EntityProfile()
        {
            CreateMap<UserEntity, UserDTO>();
            CreateMap<AccountEntity, AccountDTO>();
            CreateMap<AddedContentEntity, AddedContentDTO>();
        }
    }
}
