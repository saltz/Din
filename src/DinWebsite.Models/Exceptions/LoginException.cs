using System;

namespace DinWebsite.ExternalModels.Exceptions
{
    public class LoginException : Exception
    {
        public LoginException(string message) : base(message)
        {
        }
    }
}