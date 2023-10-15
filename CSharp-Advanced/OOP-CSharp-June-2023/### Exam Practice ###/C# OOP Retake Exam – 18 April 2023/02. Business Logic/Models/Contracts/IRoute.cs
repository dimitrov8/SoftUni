namespace EDriveRent.Models.Contracts
{
    public interface IRoute
    {
        string StartPoint { get; }

        string EndPoint { get; }

        double Length { get; }

        int RouteId { get; }

        bool IsLocked { get; }

        void LockRoute();
    }
}