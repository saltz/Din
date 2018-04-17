using System.Collections.Generic;

namespace Din.ExternalModels.Entities
{
    public class User
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Account Account { get; set; }

        public User() { }

        public User(string firstname, string lastname)
        {
            FirstName = firstname;
            LastName = lastname;
        }
    }

    public class Account
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Hash { get; set; }
        public AccountRoll Role { get; set; }
        public User User { get; set; }
        public ICollection<AddedContent> AddedContent { get; set; }
        public int UserRef { get; set; }

        public Account() { }

        public Account(string username, string hash, AccountRoll role)
        {
            Username = username;
            Hash = hash;
            Role = role;
        }
    }

    public enum AccountRoll
    {
        Admin,
        Moderator,
        User
    }
}
