using System;
using System.Collections.Generic;
using System.Text;

namespace DinWebsite.ExternalModels.Authentication
{
    public class User
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Authentication Auth { get; set; }
    }

    public class Authentication
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Hash { get; set; }
        public User User { get; set; }
        public int UserRef { get; set; }
    }
}
