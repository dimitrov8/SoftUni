using System;

namespace CarManufacturer
{
    class Car
    {
        private string make;
        private string model;
        private int year;

        public string Make
        {
            get =>  make;
            init => make = value;
        }

        public string Model
        {
            get => model;
            init => model = value;
        }

        public int Year
        {
            get => year;
            init => year = value;
        }
        

        public override string ToString()
        {
            return $"Make: {Make}{Environment.NewLine}Model: {Model}{Environment.NewLine}Year: {Year}";
        }
    }
}