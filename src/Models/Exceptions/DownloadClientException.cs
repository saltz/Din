using System;

namespace Models.Exceptions
{
    public class DownloadClientException : Exception
    {
        public DownloadClientException(string message) : base(message) { }
    }
}
