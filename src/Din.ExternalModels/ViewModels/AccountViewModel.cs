using System;
using System.Collections.Generic;
using System.Text;
using Din.ExternalModels.Entities;
using UAParser;

namespace Din.ExternalModels.ViewModels
{
    public class AccountViewModel
    {
        public User User { get; set; }
        public ClientInfo Client { get; set; }
        public ICollection<AddedContent> AddedContent { get; set; }
    }
}
