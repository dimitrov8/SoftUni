namespace Telephony.Models
{
    public interface ISmartPhone : IStationaryPhone
    {
        string Browse(string url);
    }
}