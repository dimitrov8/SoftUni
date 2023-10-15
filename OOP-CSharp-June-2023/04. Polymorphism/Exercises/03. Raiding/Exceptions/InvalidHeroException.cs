namespace Raiding.Exceptions
{
    using System;

    public class InvalidHeroException : Exception
    {
        public InvalidHeroException(string message) : base(message)
        {
        }
    }
}