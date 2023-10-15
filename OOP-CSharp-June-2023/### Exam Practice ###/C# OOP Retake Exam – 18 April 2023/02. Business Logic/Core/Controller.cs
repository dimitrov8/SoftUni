namespace EDriveRent.Core
{
    using Contracts;
    using Models;
    using Models.Contracts;
    using Repositories;
    using Repositories.Contracts;
    using System.Linq;
    using System.Text;
    using Utilities;

    public class Controller : IController
    {
        private readonly IRepository<IUser> users;
        private readonly IRepository<IVehicle> vehicles;
        private readonly IRepository<IRoute> routes;

        public Controller()
        {
            this.users = new UserRepository();
            this.vehicles = new VehicleRepository();
            this.routes = new RouteRepository();
        }

        public string RegisterUser(string firstName, string lastName, string drivingLicenseNumber)
        {
            IUser user = this.users.FindById(drivingLicenseNumber);

            if (user != null)
                return string.Format(OutputMessages.USER_ALREADY_REGISTERED_WITH_THAT_DRIVING_LICENSE,
                    drivingLicenseNumber);

            this.users.AddModel(new User(firstName, lastName, drivingLicenseNumber));
            return string.Format(OutputMessages.USER_SUCCESSFULLY_REGISTERED, firstName, lastName,
                drivingLicenseNumber);
        }

        public string UploadVehicle(string vehicleType, string brand, string model, string licensePlateNumber)
        {
            if (vehicleType != nameof(PassengerCar) && vehicleType != nameof(CargoVan))
                return string.Format(OutputMessages.VEHICLE_TYPE_NOT_SUPPORTED, vehicleType);

            IVehicle vehicle = this.vehicles.FindById(licensePlateNumber);

            if (vehicle != null)
                return string.Format(OutputMessages.VEHICLE_LICENSE_PLATE_EXISTS, licensePlateNumber);

            if (vehicleType == nameof(PassengerCar))
            {
                vehicle = new PassengerCar(brand, model, licensePlateNumber);
            }
            else if (vehicleType == nameof(CargoVan))
            {
                vehicle = new CargoVan(brand, model, licensePlateNumber);
            }

            this.vehicles.AddModel(vehicle);
            return string.Format(OutputMessages.VEHICLE_SUCCESSFULLY_UPLOADED, brand, model, licensePlateNumber);
        }

        public string AllowRoute(string startPoint, string endPoint, double length)
        {
            int routeId = this.routes.GetAll().Count + 1;

            IRoute existingRoute = this.routes
                .GetAll()
                .FirstOrDefault(r => r.StartPoint == startPoint && r.EndPoint == endPoint);

            if (existingRoute != null)
            {
                if (existingRoute.Length == length)
                    return string.Format(OutputMessages.ROUTE_EXISTS, startPoint, endPoint, length);

                if (existingRoute.Length < length)
                    return string.Format(OutputMessages.SHORTER_ROUTE_EXISTS, startPoint, endPoint);
                
                if (existingRoute.IsLocked)
                    return string.Format(OutputMessages.ROUTE_IS_LOCKED, routeId);

                if (existingRoute.Length > length)
                {
                    existingRoute.LockRoute();
                }
            }

            this.routes.AddModel(new Route(startPoint, endPoint, length, routeId));

            return string.Format(OutputMessages.ROUTE_UNLOCKED, startPoint, endPoint, length);
        }

        public string MakeTrip(string drivingLicenseNumber, string licensePlateNumber, string routeId, bool isAccidentHappened)
        {
            IUser user = this.users.FindById(drivingLicenseNumber);
            IVehicle vehicle = this.vehicles.FindById(licensePlateNumber);
            IRoute route = this.routes.FindById(routeId);

            if (user.IsBlocked)
                return string.Format(OutputMessages.USER_IS_BLOCKED, drivingLicenseNumber);

            if (vehicle.IsDamaged)
                return string.Format(OutputMessages.VEHICLE_IS_DAMAGED, licensePlateNumber);

            if (route.IsLocked)
                return string.Format(OutputMessages.ROUTE_IS_LOCKED, routeId);

            vehicle.Drive(route.Length);

            if (isAccidentHappened)
            {
                vehicle.ChangeStatus();
                user.DecreaseRating();
            }
            else
            {
                user.IncreaseRating();
            }

            return vehicle.ToString();
        }


        public string RepairVehicles(int count)
        {
            var damagedVehicles = this.vehicles.GetAll().Where(v => v.IsDamaged).OrderBy(v => v.Brand).ThenBy(v => v.Model).ToList();

            int vehiclesCount = damagedVehicles.Count < count ? damagedVehicles.Count : count;

            var vehiclesToRepair = damagedVehicles.Take(vehiclesCount);

            foreach (var vehicle in vehiclesToRepair)
            {
                vehicle.ChangeStatus();
                vehicle.Recharge();
            }

            return string.Format(OutputMessages.VEHICLES_SUCCESSFULLY_REPAIRED, vehiclesCount);
        }

        public string UsersReport()
        {
            var sb = new StringBuilder();

            sb.AppendLine("*** E-Drive-Rent ***");
            foreach (var user in this.users
                         .GetAll()
                         .OrderByDescending(u => u.Rating)
                         .ThenBy(u => u.LastName)
                         .ThenBy(u => u.FirstName))
            {
                sb.AppendLine(user.ToString());
            }

            return sb.ToString().Trim();
        }
    }
}