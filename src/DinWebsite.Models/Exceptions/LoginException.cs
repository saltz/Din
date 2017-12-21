using System;

namespace DinWebsite.Models.Exceptions
{
    public class LoginException : Exception
    {
        public LoginException(string message) : base(message) { }
    }
}
