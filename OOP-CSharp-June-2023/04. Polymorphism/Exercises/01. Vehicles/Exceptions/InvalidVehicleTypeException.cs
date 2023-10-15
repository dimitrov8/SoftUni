namespace Vehicles.Exceptions
{
    using System;

    public class InvalidVehicleTypeException : Exception
    {
        private const string INVALID_VEHICLE_TYPE_EXCEPTION = "Vehicle type not supported!";

        public InvalidVehicleTypeException() : base(INVALID_VEHICLE_TYPE_EXCEPTION)
        {
        }

        public InvalidVehicleTypeException(string message) : base(INVALID_VEHICLE_TYPE_EXCEPTION)
        {
        }
    }
}