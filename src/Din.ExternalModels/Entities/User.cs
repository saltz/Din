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

        public User(string firstname, string lastname, Account account)
        {
            FirstName = firstname;
            LastName = lastname;
            Account = account;
        }
    }

    public class Account
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Hash { get; set; }
        public AccountRoll Role { get; set; }
        public User User { get; set; }
        public AccountImage Image { get; set; }
        public List<AddedContent> AddedContent { get; set; }
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
        User,
        Moderator,
        Admin
    }

    public class AccountImage
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
        public Account Account { get; set; }
        public int AccountRef { get; set; }


        public AccountImage() { }

        public AccountImage(int iD, string name, byte[] data)
        {
            ID = iD;
            Name = name;
            Data = data;
        }
    }
}
