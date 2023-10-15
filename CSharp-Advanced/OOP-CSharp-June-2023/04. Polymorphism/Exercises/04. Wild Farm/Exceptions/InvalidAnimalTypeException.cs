namespace WildFarm.Exceptions
{
    using System;

    public class InvalidAnimalTypeException : Exception
    {
        public InvalidAnimalTypeException(string message) : base(message)
        {
        }
    }
}