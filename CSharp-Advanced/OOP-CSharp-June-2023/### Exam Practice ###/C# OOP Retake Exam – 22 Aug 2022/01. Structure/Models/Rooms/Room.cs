namespace BookingApp.Models.Rooms
{
    using Contracts;
    using System;
    using Utilities.Messages;

    public abstract class Room : IRoom
    {
        private double pricePerNight;

        protected Room(int bedCapacity)
        {
            this.BedCapacity = bedCapacity;
            this.PricePerNight = 0;
        }

        public int BedCapacity { get; private set; }

        public double PricePerNight
        {
            get => this.pricePerNight;
            private set
            {
                if (value < 0)
                    throw new ArgumentException(ExceptionMessages.PricePerNightNegative);

                this.pricePerNight = value;
            }
        }

        public void SetPrice(double price) => this.PricePerNight = price;
    }
}