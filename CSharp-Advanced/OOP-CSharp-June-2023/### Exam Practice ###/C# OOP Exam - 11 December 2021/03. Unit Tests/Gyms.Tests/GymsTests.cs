namespace Gyms.Tests
{
    using NUnit.Framework;
    using System;

    public class GymsTests
    {
        private string name;
        private int capacity;
        private Gym gym;

        [SetUp]
        public void SetUp()
        {
            this.name = "Best Gym";
            this.capacity = 10;

            this.gym = new Gym(this.name, this.capacity);
        }

        [Test]
        public void Test_Constructor_Works_Correct()
        {
            Assert.AreEqual("Best Gym", this.gym.Name);
            Assert.AreEqual(10, this.gym.Capacity);
            Assert.AreEqual(0, this.gym.Count);
        }

        [TestCase(null)]
        [TestCase("")]
        public void Test_Throw_Exception_If_Name_Is_Null_Or_Empty(string invalidName)
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                this.gym = new Gym(invalidName, 10);
            });

            Assert.AreEqual("Invalid gym name. (Parameter 'value')", ex.Message);
        }

        [Test]
        public void Test_Throw_Exception_If_Size_Is_Negative()
        {
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                this.gym = new Gym("Gym name", -1);
            });

            Assert.AreEqual("Invalid gym capacity.", ex.Message);
        }

        [Test]
        public void Test_Throw_Exception_If_Capacity_Is_Full_And_Trying_To_Add_Athlete()
        {
            var athlete1 = new Athlete("Mike");
            var athlete2 = new Athlete("John");

            this.gym = new Gym(this.gym.Name, 1);
            this.gym.AddAthlete(athlete1);

            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                this.gym.AddAthlete(athlete2);
            });

            Assert.AreEqual("The gym is full.", ex.Message);
        }

        [Test]
        public void Test_Add_Athlete_Works_Correct()
        {
            var athlete = new Athlete("Mike");

            this.gym.AddAthlete(athlete);
            Assert.AreEqual(1, this.gym.Count);
        }

        [Test]
        public void Test_Throw_Exception_If_Trying_To_Remove_Not_Existing_Athlete()
        {
            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                this.gym.RemoveAthlete("notExistingAthleteName");
            });

            Assert.AreEqual("The athlete notExistingAthleteName doesn't exist.", ex.Message);
        }

        [Test]
        public void Test_Remove_Athlete_Works_Correct()
        {
            var athlete = new Athlete("Mike");

            this.gym.AddAthlete(athlete);
            this.gym.RemoveAthlete("Mike");
            Assert.AreEqual(0, this.gym.Count);
        }

        [Test]
        public void Test_Throw_Exception_Injure_Athlete_Not_Existing()
        {
            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                this.gym.InjureAthlete("notExistingAthleteName");
            });

            Assert.AreEqual($"The athlete notExistingAthleteName doesn't exist.", ex.Message);
        }

        [Test]
        public void Test_Injure_Athlete_Works_Correct()
        {
            var athlete = new Athlete("Mike");
            
            this.gym.AddAthlete(athlete);

            Assert.AreEqual(athlete, this.gym.InjureAthlete(athlete.FullName));
            Assert.IsTrue(athlete.IsInjured);
        }

        [Test]
        public void Test_Report_Works_Correct()
        {
            this.gym = new Gym("Best Gym", 10);
            var athlete1 = new Athlete("Mike");
            var athlete2 = new Athlete("Liam");
            var athlete3 = new Athlete("Leo");

            this.gym.AddAthlete(athlete1);
            this.gym.AddAthlete(athlete2);
            this.gym.AddAthlete(athlete3);
            this.gym.InjureAthlete("Liam");

            string expectedResult = $"Active athletes at {this.gym.Name}: {athlete1.FullName}, {athlete3.FullName}";
            string actualResult = this.gym.Report();

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}