using AutoMapper;
using Din.Data.Entities;
using Din.Mapping.Converters;
using Din.Service.Clients.ResponseObjects;
using Din.Service.Dto.Account;
using Din.Service.Dto.Context;

namespace Din.Mapping.Profiles
{
    public class DtoProfile : Profile
    {
        public DtoProfile()
        {
            CreateMap<LoginLocationDto, LoginLocationEntity>();
            CreateMap<McCalendarResponse, CalendarItemDto>().ConvertUsing<McCalendarConverter>();
            CreateMap<TcCalendarResponse, CalendarItemDto>().ConvertUsing<TcCalendarConverter>();
        }
    }
}
