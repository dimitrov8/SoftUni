namespace BookingApp.Repositories
{
    using Contracts;
    using Models.Rooms.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class RoomRepository : IRepository<IRoom>
    {
        private readonly List<IRoom> rooms;

        public RoomRepository()
        {
            this.rooms = new List<IRoom>();
        }

        public void AddNew(IRoom model) => this.rooms.Add(model);

        public IRoom Select(string criteria) => this.rooms.FirstOrDefault(r => r.GetType().Name == criteria);

        public IReadOnlyCollection<IRoom> All() => this.rooms;
    }
}