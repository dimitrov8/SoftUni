namespace Vehicles.Exceptions
{
    using System;

    public class FuelMustBePositiveNumberException : Exception
    {
        public FuelMustBePositiveNumberException(string message) : base(message)
        {
        }
    }
}