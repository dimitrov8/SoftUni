namespace BookingApp.Repositories
{
    using Contracts;
    using Models.Hotels.Contacts;
    using System.Collections.Generic;
    using System.Linq;

    public class HotelRepository : IRepository<IHotel>
    {
        private readonly List<IHotel> hotels;

        public HotelRepository()
        {
            this.hotels = new List<IHotel>();
        }

        public void AddNew(IHotel model) => this.hotels.Add(model);

        public IHotel Select(string criteria) => this.hotels.FirstOrDefault(h => h.FullName == criteria);

        public IReadOnlyCollection<IHotel> All() => this.hotels;
    }
}