namespace EDriveRent.Repositories
{
    using Contracts;
    using Models.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class VehicleRepository : IRepository<IVehicle>
    {
        private List<IVehicle> vehicles;

        public VehicleRepository(List<IVehicle> vehicles)
        {
            this.vehicles = vehicles;
        }

        public void AddModel(IVehicle vehicle)
        {
            this.vehicles.Add(vehicle);
        }

        public bool RemoveById(string identifier) => this.vehicles.Remove(this.FindById(identifier));

        public IVehicle FindById(string identifier)
            => this.vehicles.FirstOrDefault(v => v.LicensePlateNumber == identifier);

        public IReadOnlyCollection<IVehicle> GetAll() => throw new NotImplementedException();
    }
}