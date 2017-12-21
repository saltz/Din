using System;

namespace DinWebsite.ExternalModels.Exceptions
{
    public class DownloadClientException : Exception
    {
        public DownloadClientException(string message) : base(message)
        {
        }
    }
}