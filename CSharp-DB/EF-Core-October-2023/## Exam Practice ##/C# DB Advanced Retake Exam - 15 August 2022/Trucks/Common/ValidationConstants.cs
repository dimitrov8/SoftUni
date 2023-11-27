namespace Trucks.Common;

public static class ValidationConstants
{
    // Truck
    public const int TRUCK_REGISTRATION_NUMBER_LENGTH = 8;

    public const string REGEX_STRING_TRUCK_REGISTRATION_NUMBER = @"^[A-Z]{2}\d{4}[A-Z]{2}";

    public const int TRUCK_VIN_NUMBER_LENGTH = 17;

    public const int TRUCK_TANK_CAPACITY_MIN = 950;
    public const int TRUCK_TANK_CAPACITY_MAX = 1420;

    public const int TRUCK_CARGO_CAPACITY_MIN = 5000;
    public const int TRUCK_CARGO_CAPACITY_MAX = 29000;

    public const int TRUCK_CATEGORY_TYPE_MIN_VALUE = 0;
    public const int TRUCK_CATEGORY_TYPE_MAX_VALUE = 3;

    public const int TRUCK_MAKE_TYPE_MIN_VALUE = 0;
    public const int TRUCK_MAKE_TYPE_MAX_VALUE = 4;

    // Client
    public const int CLIENT_NAME_MIN_LENGTH = 3;
    public const int CLIENT_NAME_MAX_LENGTH = 40;

    public const int CLIENT_NATIONALITY_MIN_LENGTH = 2;
    public const int CLIENT_NATIONALITY_MAX_LENGTH = 40;

    // Despatcher
    public const int DESPATCHER_NAME_MIN_LENGTH = 2;
    public const int DESPATCHER_NAME_MAX_LENGTH = 40;
}