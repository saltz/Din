using AutoMapper;
using Din.Service.Clients.ResponseObjects;
using Din.Service.Dto.Account;

namespace Din.Mapping.Converters
{
    public class McCalendarConverter : ITypeConverter<McCalendarResponse, CalendarItemDto>
    {
        public CalendarItemDto Convert(McCalendarResponse source, CalendarItemDto destination, ResolutionContext context)
        {
            return new CalendarItemDto
            {
                Title = source.Title,
                Downloaded = source.Downloaded,
                Start = source.PhysicalRelease,
                End = source.PhysicalRelease
            };
        }
    }
}
