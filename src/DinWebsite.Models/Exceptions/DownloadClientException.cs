using System;

namespace DinWebsite.Models.Exceptions
{
    public class DownloadClientException : Exception
    {
        public DownloadClientException(string message) : base(message) { }
    }
}
