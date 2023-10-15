namespace EDriveRent.Models.Contracts
{
    public interface IUser
    {
        string FirstName { get; }
        string LastName { get; }
        string DrivingLicenseNumber { get; }
        double Rating { get; }
        bool IsBlocked { get; }

        void IncreaseRating();

        void DecreaseRating();
    }
}