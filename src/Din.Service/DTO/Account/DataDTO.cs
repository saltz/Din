using System.Collections.Generic;
using Din.Service.DTO.Context;
using UAParser;

namespace Din.Service.DTO.Account
{
    public class DataDTO
    {
        public UserDTO User { get; set; }
        public AccountDTO Account { get; set; }
        public IEnumerable<AddedContentDTO> AddedContent { get; set; }
    }
}
