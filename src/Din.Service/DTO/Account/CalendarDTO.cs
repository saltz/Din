using System.Collections.Generic;
using Din.Service.Clients.ResponseObjects;

namespace Din.Service.DTO.Account
{
    public class CalendarDTO
    {
        public IEnumerable<MCCalendarResponse> MovieCalendar { get; set; }
        public IEnumerable<TCCalendarResponse> TvShowCalendar { get; set; }
    }
}
