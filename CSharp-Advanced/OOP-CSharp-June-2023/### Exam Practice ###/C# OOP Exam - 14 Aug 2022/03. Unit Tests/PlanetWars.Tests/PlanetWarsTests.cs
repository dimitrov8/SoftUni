namespace PlanetWars.Tests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Tests
    {
        [TestFixture]
        public class PlanetWarsTests
        {
            private string name;
            private double budget;
            private List<Weapon> weapons;

            [SetUp]
            public void Setup()
            {
                this.name = "Earth";
                this.budget = 200;
                this.weapons = new List<Weapon>();
            }

            [Test]
            public void Test_Planet_Constructor_Works_Correct()
            {
                var planet = new Planet(this.name, this.budget);
                Assert.AreEqual("Earth", planet.Name);
                Assert.AreEqual(200, planet.Budget);
                Assert.IsNotNull(this.weapons);
            }

            [TestCase(null)]
            [TestCase("")]
            public void Throw_Exception_If_Name_Is_Null_Or_Empty(string invalidName)
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    var planet = new Planet(invalidName, this.budget);
                });
            }

            [Test]
            public void Throw_Exception_If_Budget_Is_Negative()
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    var planet = new Planet(this.name, -1);
                });
            }

            [Test]
            public void Adding_Weapons_Works_Correct()
            {
                var planet = new Planet(this.name, this.budget);

                var weapon1 = new Weapon("Weapon 1", 10, 2);
                var weapon2 = new Weapon("Weapon 2", 20, 4);

                planet.SpendFunds(weapon1.Price);
                planet.AddWeapon(weapon1);
                planet.SpendFunds(weapon2.Price);
                planet.AddWeapon(weapon2);

                Assert.AreEqual(170, planet.Budget);
                Assert.AreEqual(2, planet.Weapons.Count);
            }

            [Test]
            public void Test_Military_Power_Works_Correct()
            {
                var planet = new Planet(this.name, this.budget);

                var weapon1 = new Weapon("Weapon 1", 10, 2);
                var weapon2 = new Weapon("Weapon 2", 20, 4);

                planet.AddWeapon(weapon1);
                planet.AddWeapon(weapon2);

                Assert.AreEqual(6, planet.Weapons.Sum(w => w.DestructionLevel));
            }

            [Test]
            public void Test_Throw_Exception_If_Weapon_Is_Too_Expensive()
            {
                var planet = new Planet(this.name, 20);

                var weapon1 = new Weapon("Weapon 1", 40, 2);

                var ex = Assert.Throws<InvalidOperationException>(() =>
                {
                    planet.AddWeapon(weapon1);
                    planet.SpendFunds(weapon1.Price);
                });

                Assert.AreEqual(20, planet.Budget);
                Assert.AreEqual("Not enough funds to finalize the deal.", ex.Message);
            }

            [Test]
            public void Test_Profit_Works_Correct()
            {
                var planet = new Planet(this.name, 20);
                planet.Profit(20);

                Assert.AreEqual(40, planet.Budget);
            }

            [Test]
            public void Test_Throw_Exception_If_Weapon_Exists()
            {
                var planet = new Planet(this.name, this.budget);
                var weapon1 = new Weapon("Weapon 1", 20, 2);

                planet.AddWeapon(weapon1);

                var ex = Assert.Throws<InvalidOperationException>(() =>
                {
                    planet.AddWeapon(weapon1);
                });

                Assert.AreEqual($"There is already a {weapon1.Name} weapon.", ex.Message);
            }

            [Test]
            public void Test_Remove_weapon_Works_Correct()
            {
                var planet = new Planet(this.name, this.budget);
                var weapon1 = new Weapon("Weapon 1", 20, 2);

                planet.AddWeapon(weapon1);

                planet.RemoveWeapon(weapon1.Name);

                planet.RemoveWeapon("nonExistingWeapon");

                Assert.AreEqual(0, planet.Weapons.Count);
            }

            [Test]
            public void Test_Throw_Exception_If_Trying_To_Upgrade_Non_Existing_Weapon()
            {
                var planet = new Planet(this.name, this.budget);

                var ex = Assert.Throws<InvalidOperationException>(() =>
                {
                    planet.UpgradeWeapon("nonExistingWeapon");
                });

                Assert.AreEqual(0, planet.Weapons.Count);
                Assert.AreEqual($"nonExistingWeapon does not exist in the weapon repository of {planet.Name}", ex.Message);
            }

            [Test]
            public void Test_Upgrade_Weapon_Works_Correct()
            {
                var planet = new Planet(this.name, this.budget);
                var weapon1 = new Weapon("Weapon 1", 20, 2);
                planet.AddWeapon(weapon1);
                planet.UpgradeWeapon(weapon1.Name);

                Assert.AreEqual(3, weapon1.DestructionLevel);
            }

            [Test]
            public void Test_Destruct_Opponent_Works_Correct()
            {
                var planet1 = new Planet(this.name, this.budget);
                var weapon = new Weapon("Weapon 1", 10, 2);
                planet1.AddWeapon(weapon);

                var planet2 = new Planet("Mars", 100);

                string msg = planet1.DestructOpponent(planet2);

                Assert.AreEqual($"{planet2.Name} is destructed!", msg);
            }

            [Test]
            public void Test_Throw_Exception_Destruct_Opponent_With_More_Or_Equal_Power()
            {
                var planet1 = new Planet(this.name, this.budget);
                var weapon = new Weapon("Weapon 1", 10, 2);
                planet1.AddWeapon(weapon);

                var planet2 = new Planet("Mars", 100);
                planet2.AddWeapon(weapon);

                var ex = Assert.Throws<InvalidOperationException>(() =>
                {
                    planet1.DestructOpponent(planet2);
                });

                Assert.AreEqual($"{planet2.Name} is too strong to declare war to!", ex.Message);
            }

            [Test]
            public void Throw_Exception_If_Weapon_Price_Is_Negative()
            {
                var ex = Assert.Throws<ArgumentException>(() =>
                {
                    var weapon = new Weapon("Weapon 1", -1, 2);
                });
            }

            [TestCase(10)]
            [TestCase(11)]
            public void Test_Weapon_Is_Nuclear_Works_Correct(int destructionLevel)
            {
                var weapon = new Weapon("Weapon 1", 200, destructionLevel);
                
                Assert.IsTrue(weapon.IsNuclear);
            }
        }
    }
}