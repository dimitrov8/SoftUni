namespace VehicleGarage.Tests
{
    using NUnit.Framework;
    using System.Collections.Generic;

    public class Tests
    {
        private List<Vehicle> emptyVehiclesList;

        private readonly List<Vehicle> vehicles = new List<Vehicle>
        {
            new Vehicle("Tesla", "Model Y", "001"),
            new Vehicle("Porsche", "Taycan", "002"),
            new Vehicle("BMW", "i4", "003")
        };

        private Garage garage;

        [SetUp]
        public void Setup()
        {
            this.garage = new Garage(5);
            this.emptyVehiclesList = new List<Vehicle>();
        }

        [Test]
        public void Constructor_Should_Initialize_Correctly()
        {
            Assert.AreEqual(5, this.garage.Capacity, "Garage capacity is not set correctly!");
            Assert.AreEqual(0, this.emptyVehiclesList.Count, "Initializing empty list of vehicles is not working properly!");
        }

        [Test]
        public void AddVehicle_Should_Add_Vehicle_Correctly()
        {
            var carTesla = new Vehicle("Tesla", "Model Y", "001");
            var carPorsche = new Vehicle("Porsche", "Taycan", "002");

            this.garage.AddVehicle(carTesla);
            this.garage.AddVehicle(carPorsche);

            int expectedAddedVehicles = 2;
            int actualAddedVehicles = this.garage.Vehicles.Count;

            Assert.AreEqual(expectedAddedVehicles, actualAddedVehicles, "Adding vehicles is not working correctly!");
        }

        [Test]
        public void Test_Cannot_Add_Cars_If_Capacity_Is_Full()
        {
            this.garage = new Garage(2);
            var carTesla = new Vehicle("Tesla", "Model Y", "001");
            var carPorsche = new Vehicle("Porsche", "Taycan", "002");

            var additionalVehicle = new Vehicle("BMW", "i4", "003");
            this.garage.AddVehicle(carTesla);
            this.garage.AddVehicle(carPorsche);
            this.garage.AddVehicle(additionalVehicle);

            int expectedAddedCars = 2;
            int actualAddedCars = this.garage.Vehicles.Count;

            Assert.AreEqual(expectedAddedCars, actualAddedCars);
        }

        [Test]
        public void Test_Cannot_Add_Cars_With_The_Same_License()
        {
            this.garage = new Garage(3);
            var carTesla = new Vehicle("Tesla", "Model Y", "001");
            var carPorsche = new Vehicle("Porsche", "Taycan", "001");

            this.garage.AddVehicle(carTesla);
            this.garage.AddVehicle(carPorsche);

            int expectedAddedCars = 1;
            int actualAddedCars = this.garage.Vehicles.Count;

            Assert.AreEqual(expectedAddedCars, actualAddedCars);
        }

        [Test]
        public void Test_Cannot_Add_The_Same_Car()
        {
            this.garage = new Garage(5);
            var carTesla = new Vehicle("Tesla", "Model Y", "001");

            this.garage.AddVehicle(carTesla);
            this.garage.AddVehicle(carTesla);

            int expectedAddedCars = 1;
            int actualAddedCars = this.garage.Vehicles.Count;

            Assert.AreEqual(expectedAddedCars, actualAddedCars);
        }

        [Test]
        public void Test_Charge_Method_Works()
        {
            foreach (var vehicle in this.vehicles)
            {
                this.garage.AddVehicle(vehicle);
            }

            this.garage.DriveVehicle("001", 60, false);
            this.garage.DriveVehicle("003", 70, false);

            int expectedChargedVehicles = 2;
            int actualChargedVehicles = this.garage.ChargeVehicles(40);

            Assert.AreEqual(expectedChargedVehicles, actualChargedVehicles);
        }

        [Test]
        public void Test_Cannot_Drive_If_Battery_Draining_Is_Over_100()
        {
            var carTesla = new Vehicle("Tesla", "Model Y", "001");
            this.garage.AddVehicle(carTesla);

            this.garage.DriveVehicle("001", 101, false);

            int expectedBattery = 100;
            int actualBattery = carTesla.BatteryLevel;

            Assert.AreEqual(expectedBattery, actualBattery);
        }

        [Test]
        public void Test_Cannot_Drive_If_Battery_Draining_Is_More_Than_The_Vehicle_Current_Battery()
        {
            var carTesla = new Vehicle("Tesla", "Model Y", "001");
            this.garage.AddVehicle(carTesla);

            this.garage.DriveVehicle("001", 90, false);
            this.garage.DriveVehicle("001", 90, false);

            int expectedBattery = 10;
            int actualBattery = carTesla.BatteryLevel;

            Assert.AreEqual(expectedBattery, actualBattery);
        }

        [Test]
        public void Test_Drive_Method_Works()
        {
            var carTesla = new Vehicle("Tesla", "Model Y", "001");
            this.garage.AddVehicle(carTesla);

            this.garage.DriveVehicle("001", 25, false);

            int expectedBattery = 75;
            int actualBattery = carTesla.BatteryLevel;

            Assert.AreEqual(expectedBattery, actualBattery);
        }

        [Test]
        public void Test_Cannot_Drive_Damaged_Car()
        {
            var carTesla = new Vehicle("Tesla", "Model Y", "001");
            this.garage.AddVehicle(carTesla);

            this.garage.DriveVehicle("001", 25, true);
            this.garage.DriveVehicle("001", 25, true);

            int expectedBattery = 75;
            int actualBattery = carTesla.BatteryLevel;

            Assert.AreEqual(expectedBattery, actualBattery);
        }

        [Test]
        public void Test_Check_If_Vehicle_Is_Damaged_Is_Working()
        {
            var carTesla = new Vehicle("Tesla", "Model Y", "001");
            this.garage.AddVehicle(carTesla);

            this.garage.DriveVehicle("001", 25, true);

            bool expectedIsDamaged = true;
            bool actualIsDamaged = carTesla.IsDamaged;

            Assert.AreEqual(expectedIsDamaged, actualIsDamaged);
        }

        [Test]
        public void Test_Repair_Method_Works()
        {
            var vehiclesList = new List<Vehicle>
            {
                new Vehicle("Tesla", "Model Y", "001"),
                new Vehicle("Porsche", "Taycan", "002"),
                new Vehicle("BMW", "i4", "003")
            };

            foreach (var vehicle in vehiclesList)
            {
                this.garage.AddVehicle(vehicle);
            }

            this.garage.DriveVehicle("001", 25, true);
            this.garage.DriveVehicle("003", 35, true);

            string expectedResult = "Vehicles repaired: 2";
            string actualRepairedVehicles = this.garage.RepairVehicles();

            Assert.AreEqual(expectedResult, actualRepairedVehicles);
        }

        [Test]
        public void Test_Car_Is_Not_Damaged_When_Drive()
        {
            var carTesla = new Vehicle("Tesla", "Model Y", "001");
            this.garage.AddVehicle(carTesla);

            this.garage.DriveVehicle("001", 25, false);

            Assert.AreEqual(false, carTesla.IsDamaged);
        }

        [Test]
        public void Test_Car_Repair_Is_Working_Correct()
        {
            var carTesla = new Vehicle("Tesla", "Model Y", "001");
            this.garage.AddVehicle(carTesla);
            
            this.garage.DriveVehicle("001", 25, true);

            this.garage.RepairVehicles();

            Assert.AreEqual(false, carTesla.IsDamaged);
        }
    }
}