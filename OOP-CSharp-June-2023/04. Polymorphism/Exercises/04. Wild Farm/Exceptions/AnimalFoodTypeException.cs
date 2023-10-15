namespace WildFarm.Exceptions
{
    using System;

    public class AnimalFoodTypeException : Exception
    {
        public AnimalFoodTypeException(string message) : base(message)
        {
        }
    }
}