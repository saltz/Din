using AutoMapper;
using Din.Service.Clients.ResponseObjects;
using Din.Service.Dto.Account;

namespace Din.Mapping.Converters
{
    public class TcCalendarConverter : ITypeConverter<TcCalendarResponse, CalendarItemDto>
    {
        public CalendarItemDto Convert(TcCalendarResponse source, CalendarItemDto destination, ResolutionContext context)
        {
            return new CalendarItemDto
            {
                Title = source.Series.Title,
                Start = source.AirDate,
                End = source.AirDate
            };
        }
    }
}
