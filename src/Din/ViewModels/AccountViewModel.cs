using Din.Service.DTO.Account;
using UAParser;

namespace Din.ViewModels
{
    public class AccountViewModel
    {
        public DataDTO Data { get; set; }
        public ClientInfo ClientInfo { get; set; }
        public CalendarDTO MovieCalendar { get; set; }
        public CalendarDTO TvShowCalendar { get; set; }
    }
}
