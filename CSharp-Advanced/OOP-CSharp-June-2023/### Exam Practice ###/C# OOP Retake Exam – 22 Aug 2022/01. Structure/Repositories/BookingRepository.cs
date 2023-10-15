namespace BookingApp.Repositories
{
    using Contracts;
    using Models.Bookings.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BookingRepository : IRepository<IBooking>
    {
        private readonly List<IBooking> bookingList;

        public BookingRepository()
        {
            this.bookingList = new List<IBooking>();
        }

        public void AddNew(IBooking model) => this.bookingList.Add(model);

        public IBooking Select(string criteria) => this.bookingList.FirstOrDefault(b => b.BookingNumber.ToString() == criteria);

        public IReadOnlyCollection<IBooking> All() => this.bookingList;
    }
}