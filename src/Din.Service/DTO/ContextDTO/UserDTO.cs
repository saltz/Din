using System.Collections.Generic;
using Din.Data.Entities;

namespace Din.Service.DTO.ContextDTO
{
    public class UserDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AccountDTO Account { get; set; }
    }

    public class AccountDTO
    {
        public string Username { get; set; }
        internal AccountRoll Role { get; set; }
        public AccountImage Image { get; set; }
        public ICollection<AddedContentDTO> AddedContent { get; set; }
    }

    internal enum AccountRoll
    {
        User,
        Moderator,
        Admin
    }

    public class AccountImageDTO
    {
        public string Name { get; set; }
        public byte[] Data { get; set; }
    }
}
