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
}
