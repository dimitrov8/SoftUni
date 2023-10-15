namespace EDriveRent.Models
{
    using Contracts;
    using System;
    using Utilities;

    public class User : IUser
    {
        private string firstName;
        private string lastName;
        private string drivingLicenseNumber;
        private double rating;
        private bool isBlocked;

        public User(string firstName, string lastName, string drivingLicenseNumber)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.DrivingLicenseNumber = drivingLicenseNumber;
            this.rating = 0;
            this.isBlocked = false;
        }

        public string FirstName
        {
            get => this.firstName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(string.Format(ExceptionMessages.STRING_IS_NOT_VALID,
                        nameof(this.FirstName)));

                this.firstName = value;
            }
        }

        public string LastName
        {
            get => this.lastName;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException(string.Format(ExceptionMessages.STRING_IS_NOT_VALID,
                        nameof(this.LastName)));

                this.lastName = value;
            }
        }

        public string DrivingLicenseNumber
        {
            get => this.drivingLicenseNumber;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException(ExceptionMessages
                        .DRIVING_LICENSE_NUMBER_IS_NOT_VALID);

                this.drivingLicenseNumber = value;
            }
        }

        public double Rating => this.rating;

        public bool IsBlocked => this.isBlocked;

        public void IncreaseRating()
        {
            if (this.rating < 10)
            {
                this.rating += 0.5;
            }
        }

        public void DecreaseRating()
        {
            if (this.rating < 2)
            {
                this.rating = 0;
                this.isBlocked = true;
            }
            else
            {
                this.rating -= 2;
            }
        }

        public override string ToString()
            => $"{this.FirstName} {this.LastName} Driving license: {this.drivingLicenseNumber} Rating: {this.rating}";
    }
}