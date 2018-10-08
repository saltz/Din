using AutoMapper;
using Din.Mapping.Converters;
using Din.Service.Clients.ResponseObjects;
using Din.Service.Dto.Context;
using Din.Service.DTO.Content;

namespace Din.Mapping.Profiles
{
    public class ResponseToDtoProfile : Profile
    {
        public ResponseToDtoProfile()
        {
            CreateMap<IpStackResponse, LoginLocationDto>();
            CreateMap<McCalendarResponse, CalendarItemDto>().ConvertUsing<McCalendarConverter>();
            CreateMap<TcCalendarResponse, CalendarItemDto>().ConvertUsing<TcCalendarConverter>();
        }
    }
}
