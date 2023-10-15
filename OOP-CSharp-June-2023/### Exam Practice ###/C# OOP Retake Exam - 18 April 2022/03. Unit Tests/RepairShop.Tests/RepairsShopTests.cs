namespace RepairShop.Tests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    public class Tests
    {
        private string name;
        private int mechanicsAvailable;
        private Garage garage;
        private readonly Car teslaCar = new Car("Tesla", 2);

        [SetUp]
        public void SetUp()
        {
            this.name = "Garage 1";
            this.mechanicsAvailable = 2;
            this.garage = new Garage(this.name, this.mechanicsAvailable);
        }

        [Test]
        public void Test_Constructor_Works_Correct()
        {
            Assert.AreEqual("Garage 1", this.garage.Name);
            Assert.AreEqual(2, this.garage.MechanicsAvailable);
            Assert.IsNotNull(this.garage.CarsInGarage);
        }

        [TestCase(null)]
        [TestCase("")]
        public void Test_Throw_Exception_If_Name_Is_Null_Or_Empty(string invalidName)
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                this.garage = new Garage(invalidName, 2);
            });
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void Test_Throw_Exception_If_Mechanics_Are_Zero_Or_Negative(int invalidMechanics)
        {
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                this.garage = new Garage(this.name, invalidMechanics);
            });

            Assert.AreEqual("At least one mechanic must work in the garage.", ex.Message);
        }

        [Test]
        public void Test_Throw_Exception_If_Mechanics_Are_Not_Enough()
        {
            this.garage = new Garage("Garage 1", 1);
            var car = new Car("Mercedes", 1);
            var extraCar = this.teslaCar;
            this.garage.AddCar(car);

            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                this.garage.AddCar(extraCar);
            });

            Assert.AreEqual("No mechanic available.", ex.Message);
        }

        [Test]
        public void Test_Throw_Exception_If_Trying_To_Fix_Not_Existing_Car()
        {
            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                this.garage.FixCar("Maserati");
            });

            Assert.AreEqual("The car Maserati doesn't exist.", ex.Message);
        }

        [Test]
        public void Test_Fix_Car_Works_Correct()
        {
            var car = new Car("Tesla", 2);
            this.garage.AddCar(car);

            this.garage.FixCar("Tesla");

            Assert.AreEqual(0, car.NumberOfIssues);
        }

        [Test]
        public void Test_Throw_Exception_If_Trying_To_Remove_Cars_Which_Are_Not_Fixed()
        {
            var car = new Car("Tesla", 2);
            this.garage.AddCar(car);

            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                this.garage.RemoveFixedCar();
            });

            Assert.AreEqual("No fixed cars available.", ex.Message);
        }

        [Test]
        public void Test_Remove_Fixed_Cars_Works_Correct()
        {
            this.garage.AddCar(this.teslaCar);
            this.garage.FixCar("Tesla");

            this.garage.RemoveFixedCar();

            Assert.AreEqual(0, this.garage.CarsInGarage);
        }

        [Test]
        public void Test_Report_Works_Correct()
        {
            this.garage = new Garage("Garage 1", 3);
            var cars = new List<Car>();
            var fixedMercedes = new Car("Mercedes", 0);
            var brokenTesla = new Car("Tesla", 2);
            var brokenMaserati = new Car("Maserati", 3);

            cars.Add(fixedMercedes);
            cars.Add(brokenTesla);
            cars.Add(brokenMaserati);

            foreach (var car in cars)
            {
                this.garage.AddCar(car);
            }

            string report = this.garage.Report();

            Assert.AreEqual("There are 2 which are not fixed: Tesla, Maserati.", report);
        }
    }
}