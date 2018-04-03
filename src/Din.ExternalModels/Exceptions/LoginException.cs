using System;

namespace Din.ExternalModels.Exceptions
{
    public class LoginException : Exception
    {
        public LoginException(string message) : base(message)
        {
        }
    }
}