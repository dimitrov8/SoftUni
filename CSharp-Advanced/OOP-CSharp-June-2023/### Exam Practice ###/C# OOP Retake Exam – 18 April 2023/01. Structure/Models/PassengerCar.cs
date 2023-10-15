namespace EDriveRent.Models
{
    public class PassengerCar : Vehicle
    {
        private const int MAX_MILEAGE = 450;

        public PassengerCar(string brand, string model, string licensePlateNumber) :
            base(brand, model, MAX_MILEAGE, licensePlateNumber)
        {
        }
    }
}