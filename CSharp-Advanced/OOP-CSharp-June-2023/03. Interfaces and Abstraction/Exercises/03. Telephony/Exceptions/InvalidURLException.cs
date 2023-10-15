namespace Telephony.Exceptions
{
    using System;

    public class InvalidUrlException : Exception
    {
        private const string DEFAULT_EXCEPTION_MESSAGE = "Invalid URL!";
        
        public InvalidUrlException() : base(DEFAULT_EXCEPTION_MESSAGE)
        {
        }

        public InvalidUrlException(string message) : base(message)
        {
        }
    }
}