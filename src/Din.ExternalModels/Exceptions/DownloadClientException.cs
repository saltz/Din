using System;

namespace Din.ExternalModels.Exceptions
{
    public class DownloadClientException : Exception
    {
        public DownloadClientException(string message) : base(message)
        {
        }
    }
}