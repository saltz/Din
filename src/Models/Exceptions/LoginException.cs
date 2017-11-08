using System;
using System.Runtime.Serialization;

namespace Models.Exceptions
{
    public class LoginException : Exception
    {
        public LoginException()
        {
        }

        public LoginException(string message) : base(message)
        {
        }
    }
}
