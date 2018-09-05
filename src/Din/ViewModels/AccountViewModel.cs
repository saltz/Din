using System.Collections.Generic;
using Din.Data.Entities;
using Din.Service.DTO.ContextDTO;
using UAParser;

namespace Din.ViewModels
{
    public class AccountViewModel
    {
        public UserDTO User { get; set; }
        public ClientInfo Client { get; set; }
        public IEnumerable<AddedContentDTO> AddedContent { get; set; }
    }
}
