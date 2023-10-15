namespace Vehicles.Exceptions
{
    using System;

    public class CannotFitFuelException : Exception
    {
        public CannotFitFuelException(string message) : base(message)
        {
        }
    }
}