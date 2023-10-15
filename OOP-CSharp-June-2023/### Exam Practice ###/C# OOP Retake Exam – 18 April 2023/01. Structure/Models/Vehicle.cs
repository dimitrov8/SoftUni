namespace EDriveRent.Models
{
    using Contracts;
    using System;
    using Utilities.ExceptionMessages;

    public abstract class Vehicle : IVehicle
    {
        private string brand;
        private string model;
        private double maxMilage;
        private string licensePlateNumber;
        private int batteryLevel;
        private bool isDamaged;

        protected Vehicle(string brand, string model, double maxMileage, string licensePlateNumber)
        {
            this.Brand = brand;
            this.Model = model;
            this.maxMilage = maxMileage;
            this.LicensePlateNumber = licensePlateNumber;
            this.batteryLevel = 100;
            this.isDamaged = false;
        }

        public string Brand
        {
            get => this.brand;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(string.Format(ExceptionMessages.STRING_IS_NOT_VALID,
                        nameof(this.brand)));

                this.brand = value;
            }
        }

        public string Model
        {
            get => this.model;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(string.Format(ExceptionMessages.STRING_IS_NOT_VALID,
                        nameof(this.Model)));

                this.model = value;
            }
        }

        public double MaxMileage => this.maxMilage;

        public string LicensePlateNumber
        {
            get => this.licensePlateNumber;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException(string.Format(ExceptionMessages.STRING_IS_NOT_VALID,
                        nameof(this.LicensePlateNumber)));

                this.licensePlateNumber = value;
            }
        }

        public int BatteryLevel => this.batteryLevel;

        public bool IsDamaged => this.isDamaged;

        public void Drive(double mileage)
        {
            double percentage = Math.Round(mileage / this.maxMilage) * 100;
            this.batteryLevel -= (int)percentage;

            if (this.GetType().Name == nameof(CargoVan))
            {
                this.batteryLevel -= 5;
            }
        }

        public void Recharge() => this.batteryLevel = 100;

        public void ChangeStatus()
            => this.isDamaged = this.isDamaged == false ? this.isDamaged = true : this.isDamaged = false;

        public override string ToString()
        {
            string vehicleCondition = this.isDamaged == false ? "OK" : "damaged";

            return
                $"{this.Brand} {this.Model} License plate: {this.LicensePlateNumber} Battery: {this.BatteryLevel}% Status: {vehicleCondition}";
        }
    }
}