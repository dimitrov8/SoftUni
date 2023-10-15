namespace EDriveRent.Repositories
{
    using Contracts;
    using Models.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class VehicleRepository : IRepository<IVehicle>
    {
        private readonly List<IVehicle> vehicles;

        public VehicleRepository()
        {
            this.vehicles = new List<IVehicle>();
        }

        public void AddModel(IVehicle vehicle)
        {
            this.vehicles.Add(vehicle);
        }

        public bool RemoveById(string identifier) => this.vehicles.Remove(this.FindById(identifier));

        public IVehicle FindById(string identifier)
            => this.vehicles.FirstOrDefault(v => v.LicensePlateNumber == identifier);

        public IReadOnlyCollection<IVehicle> GetAll() => this.vehicles;
    }
}