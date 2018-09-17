using System;
using System.Collections.Generic;
using Din.Service.Clients.ResponseObjects;

namespace Din.Service.Dto.Account
{
    public class CalendarDto
    {
        public Tuple<DateTime, DateTime> DateRange { get; set; }
        public IEnumerable<CalendarItemDto> Items { get; set; }
    }


    public class CalendarItemDto
    {
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool Downloaded { get; set; }
    }
}
