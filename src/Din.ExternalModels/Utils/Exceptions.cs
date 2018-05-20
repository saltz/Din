using System;

namespace Din.ExternalModels.Utils
{
    public class DatabaseException : Exception
    {
        public DatabaseException(string message) : base(message)
        {
        }
    }
    
    public class DownloadClientException : Exception
    {
        public DownloadClientException(string message) : base(message)
        {
        }
    }
    
    public class LoginException : Exception
    {
        public LoginException(string message) : base(message)
        {
        }
    }
}