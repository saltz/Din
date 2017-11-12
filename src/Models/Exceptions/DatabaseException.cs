using System;

namespace Models.Exceptions
{
    public class DatabaseException : Exception
    {
        public DatabaseException(string message) : base(message) { }
    }
}
