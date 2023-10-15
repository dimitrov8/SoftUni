namespace Telephony.Models
{
    using Exceptions;
    using System.Linq;

    public class StationaryPhone : IStationaryPhone
    {
        public string Call(string phoneNumber)
        {
            if (!this.IsValidPhoneNumber(phoneNumber))
                throw new InvalidPhoneNumberException();

            return $"Dialing... {phoneNumber}";
        }

        private bool IsValidPhoneNumber(string phoneNumber) => phoneNumber.All(char.IsDigit);
    }
}