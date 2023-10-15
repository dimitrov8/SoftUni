namespace BookingApp.Models.Hotels
{
    using Bookings.Contracts;
    using Contacts;
    using Repositories;
    using Repositories.Contracts;
    using Rooms.Contracts;
    using System;
    using System.Linq;
    using Utilities.Messages;

    public class Hotel : IHotel
    {
        private string fullName;
        private int category;

        public Hotel(string fullName, int category)
        {
            this.FullName = fullName;
            this.Category = category;
            this.Rooms = new RoomRepository();
            this.Bookings = new BookingRepository();
        }

        public string FullName
        {
            get => this.fullName;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException(ExceptionMessages.HotelNameNullOrEmpty);

                this.fullName = value;
            }
        }

        public int Category
        {
            get => this.category;
            private set
            {
                if (value < 1 || value > 5)
                    throw new ArgumentException(ExceptionMessages.InvalidCategory);

                this.category = value;
            }
        }

        public double Turnover => Math.Round(this.Bookings.All().Sum(b => b.ResidenceDuration * b.Room.PricePerNight), 2);
        public IRepository<IRoom> Rooms { get; set; }
        public IRepository<IBooking> Bookings { get; set; }
    }
}