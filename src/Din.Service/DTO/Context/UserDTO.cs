using System.Collections.Generic;
using Din.Data.Entities;

namespace Din.Service.DTO.Context
{
    public class UserDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AccountDTO Account { get; set; }
    }

    public class AccountDTO
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public AccountRoll Role { get; set; }
        public AccountImageDTO Image { get; set; }
        public ICollection<AddedContentDTO> AddedContent { get; set; }
    }

    public class AccountImageDTO
    {
        public string Name { get; set; }
        public byte[] Data { get; set; }
    }
}
