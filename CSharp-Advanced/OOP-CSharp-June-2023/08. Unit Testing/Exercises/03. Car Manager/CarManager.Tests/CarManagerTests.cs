namespace CarManager.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class CarManagerTests
    {
        private Car defaultCar;

        [SetUp]
        public void Setup()
        {
            this.defaultCar = new Car("Audi", "RS7", 8.7, 73);
        }

        [Test]
        public void Test_Constructor_Make_Should_Initialize_Correct()
        {
            string expectedMake = this.defaultCar.Make;
            string actualMake = this.defaultCar.Make;

            Assert.AreEqual(expectedMake, actualMake);
        }

        [Test]
        public void Test_Constructor_Model_Should_Initialize_Correct()
        {
            string expectedModel = this.defaultCar.Model;
            string actualModel = this.defaultCar.Model;

            Assert.AreEqual(expectedModel, actualModel);
        }

        [Test]
        public void Test_Constructor_Fuel_Consumption_Should_Initialize_Correct()
        {
            double expectedFuelConsumption = this.defaultCar.FuelConsumption;
            double actualFuelConsumption = this.defaultCar.FuelConsumption;

            Assert.AreEqual(expectedFuelConsumption, actualFuelConsumption);
        }

        [Test]
        public void Test_Constructor_Fuel_Capacity_Should_Initialize_Correct()
        {
            double expectedFuelConsumption = this.defaultCar.FuelCapacity;
            double actualFuelConsumption = this.defaultCar.FuelCapacity;

            Assert.AreEqual(expectedFuelConsumption, actualFuelConsumption);
        }

        [Test]
        public void Test_Constructor_Fuel_Amount_Should_Initialize_Correct()
        {
            double expectedFuelAmount = 0;
            double actualFuelAmount = this.defaultCar.FuelAmount;

            Assert.AreEqual(expectedFuelAmount, actualFuelAmount);
        }

        [TestCase(null)]
        [TestCase("")]
        public void Test_Throw_Exception_If_Make_Is_Null_Or_Empty(string invalidMake)
        {
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                var car = new Car(invalidMake, "RS7", 8.7, 73);
            });

            Assert.AreEqual("Make cannot be null or empty!", ex.Message);
        }

        [TestCase(null)]
        [TestCase("")]
        public void Test_Throw_Exception_If_Model_Is_Null_Or_Empty(string invalidModel)
        {
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                var car = new Car("Audi", invalidModel, 8.7, 73);
            });

            Assert.AreEqual("Model cannot be null or empty!", ex.Message);
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void Test_Throw_Exception_If_Fuel_Consumption_Is_Zero_Or_Negative(double invalidFuelConsumption)
        {
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                var car = new Car("Audi", "RS7", invalidFuelConsumption, 73);
            });

            Assert.AreEqual("Fuel consumption cannot be zero or negative!", ex.Message);
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void Test_Throw_Exception_If_Fuel_Capacity_Is_Zero_Or_Negative(double invalidFuelCapacity)
        {
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                var car = new Car("Audi", "RS7", 8.7, invalidFuelCapacity);
            });

            Assert.AreEqual("Fuel capacity cannot be zero or negative!", ex.Message);
        }

        [Test]
        public void Test_Refuel_Method_Should_Work_Correct()
        {
            this.defaultCar.Refuel(10);

            double expectedFuelAmount = 10;
            double actualFuelAmount = this.defaultCar.FuelAmount;

            Assert.AreEqual(expectedFuelAmount, actualFuelAmount);
        }

        [TestCase(74)]
        [TestCase(99)]
        public void Test_Refuel_Method_Should_Not_Go_Over_The_Fuel_Capacity(double wantedFuel)
        {
            this.defaultCar.Refuel(wantedFuel);

            double expectedFuelAmount = this.defaultCar.FuelCapacity;
            double actualFuelAmount = this.defaultCar.FuelAmount;

            Assert.AreEqual(expectedFuelAmount, actualFuelAmount);
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void Test_Throw_Exception_If_Zero_Or_Negative_Value_To_Refuel(double wantedFuel)
        {
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                this.defaultCar.Refuel(wantedFuel);
            });

            Assert.AreEqual("Fuel amount cannot be zero or negative!", ex.Message);
        }

        [TestCase(20.5)]
        public void Test_Drive_Method_Should_Work_Correct(double distance)
        {
            this.defaultCar.Refuel(10);
            double fuelNeeded = (distance / 100) * this.defaultCar.FuelConsumption;

            double expectedFuelAmount = this.defaultCar.FuelAmount - fuelNeeded;
            
            this.defaultCar.Drive(distance);
            double actualFuelAmount = this.defaultCar.FuelAmount;

            Assert.AreEqual(expectedFuelAmount, actualFuelAmount);
        }

        [Test]
        public void Test_Throw_Exception_If_Not_Enough_Fuel_To_Drive()
        {
            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                this.defaultCar.Drive(100);
            });

            Assert.AreEqual("You don't have enough fuel to drive!", ex.Message);
        }
    }
}