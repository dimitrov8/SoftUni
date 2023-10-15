namespace Telephony.Models
{
    using Exceptions;
    using System.Linq;

    public class SmartPhone : ISmartPhone
    {
        public string Call(string phoneNumber)
        {
            if (!this.IsValidPhoneNumber(phoneNumber))
                throw new InvalidPhoneNumberException();

            return $"Calling... {phoneNumber}";
        }

        public string Browse(string url)
        {
            if (!this.IsValidUrl(url))
                throw new InvalidUrlException();

            return $"Browsing: {url}!";
        }

        private bool IsValidPhoneNumber(string phoneNumber) => phoneNumber.All(char.IsDigit);

        private bool IsValidUrl(string url) => url.All(c => !char.IsDigit(c));
    }
}