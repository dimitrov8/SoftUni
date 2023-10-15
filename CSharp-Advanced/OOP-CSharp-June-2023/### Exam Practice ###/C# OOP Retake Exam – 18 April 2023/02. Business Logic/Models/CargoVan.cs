namespace EDriveRent.Models
{
    public class CargoVan : Vehicle
    {
        private const int MAX_MILEAGE = 180;

        public CargoVan(string brand, string model, string licensePlateNumber)
            : base(brand, model, MAX_MILEAGE, licensePlateNumber)
        {
        }
    }
}