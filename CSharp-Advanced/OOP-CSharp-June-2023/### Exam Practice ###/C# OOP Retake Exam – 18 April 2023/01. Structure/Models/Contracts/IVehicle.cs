namespace EDriveRent.Models.Contracts
{
    public interface IVehicle
    {
        string Brand { get; }

        string Model { get; }

        double MaxMileage { get; }

        string LicensePlateNumber { get; }

        int BatteryLevel { get; }

        bool IsDamaged { get; }

        void Drive(double mileage);

        void Recharge();

        void ChangeStatus();
    }
}