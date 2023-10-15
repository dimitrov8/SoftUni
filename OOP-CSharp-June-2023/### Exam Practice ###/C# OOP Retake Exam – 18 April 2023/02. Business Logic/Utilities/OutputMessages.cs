namespace EDriveRent.Utilities
{
    public class OutputMessages
    {
        public const string USER_ALREADY_REGISTERED_WITH_THAT_DRIVING_LICENSE = "{0} is already registered in our platform.";

        public const string USER_SUCCESSFULLY_REGISTERED = "{0} {1} is registered successfully with DLN-{2}";

        public const string VEHICLE_TYPE_NOT_SUPPORTED = "{0} is not accessible in our platform.";

        public const string VEHICLE_SUCCESSFULLY_UPLOADED = "{0} {1} is uploaded successfully with LPN-{2}";
        
        public const string ROUTE_EXISTS = "{0}/{1} - {2} km is already added in our platform.";

        public const string SHORTER_ROUTE_EXISTS = "{0}/{1} shorter route is already added in our platform.";

        public const string ROUTE_UNLOCKED = "{0}/{1} - {2} km is unlocked in our platform.";

        public const string USER_IS_BLOCKED = "User {0} is blocked in the platform! Trip is not allowed.";
        
        public const string VEHICLE_IS_DAMAGED = "Vehicle {0} is damaged! Trip is not allowed.";

        public const string ROUTE_IS_LOCKED = "Route {0} is locked! Trip is not allowed.";
        
        public const string VEHICLES_SUCCESSFULLY_REPAIRED = "{0} vehicles are successfully repaired!";
        
        public const string VEHICLE_LICENSE_PLATE_EXISTS = "{0} belongs to another vehicle.";
    }
}