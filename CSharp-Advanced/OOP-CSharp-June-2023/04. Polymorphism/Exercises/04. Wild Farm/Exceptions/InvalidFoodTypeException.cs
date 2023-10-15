namespace WildFarm.Exceptions
{
    using System;

    public class InvalidFoodTypeException : Exception
    {
        public InvalidFoodTypeException(string message) : base(message)
        {
        }
    }
}