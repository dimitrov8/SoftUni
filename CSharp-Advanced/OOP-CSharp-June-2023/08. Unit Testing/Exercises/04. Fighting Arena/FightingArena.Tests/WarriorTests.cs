namespace FightingArena.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class WarriorTests
    {
        private Warrior defaultWarrior;
        private const int MIN_ATTACK_HP = 30;

        [SetUp]
        public void SetUp()
        {
            this.defaultWarrior = new Warrior("Draven", 40, 100);
        }

        [TestCase("Draven")]
        [TestCase("E")]
        [TestCase("Very very very very very long name")]
        public void Test_Constructor_Should_Set_Name_Correctly(string name)
        {
            var warrior = new Warrior(name, 50, 100);

            string expectedName = name;
            string actualName = warrior.Name;

            Assert.AreEqual(expectedName, actualName);
        }

        [Test]
        public void Test_Constructor_Should_Set_Damage_Correctly()
        {
            Assert.AreEqual(40, this.defaultWarrior.Damage);
        }

        [Test]
        public void Test_Constructor_Should_Set_Health_Correctly()
        {
            Assert.AreEqual(100, this.defaultWarrior.HP);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("            ")]
        public void Test_Throw_Exception_If_Name_Is_Null_Or_WhiteSpace(string invalidName)
        {
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                var warrior = new Warrior(invalidName, 40, 100);
            }, "Name should be null or white space!");

            Assert.AreEqual("Name should not be empty or whitespace!", ex.Message);
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void Test_Throw_Exception_If_Damage_Is_Zero_Or_Negative(int invalidDamage)
        {
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                var warrior = new Warrior("Draven", invalidDamage, 100);
            }, "Damage value should be zero or below!");

            Assert.AreEqual("Damage value should be positive!", ex.Message);
        }

        [TestCase(-1)]
        public void Test_Throw_Exception_If_Health_Is_Negative(int invalidHealth)
        {
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                var warrior = new Warrior("Draven", 40, invalidHealth);
            }, "Health value should be negative!");

            Assert.AreEqual("HP should not be negative!", ex.Message);
        }

        [TestCase(MIN_ATTACK_HP)]
        [TestCase(15)]
        [TestCase(0)]
        public void Test_Throw_Exception_If_Warrior_Has_Too_Low_Health_To_Attack(int lowHealth)
        {
            var attackingWarrior = new Warrior("Olaf", 40, lowHealth);
            var defendingWarrior = new Warrior("Draven", 30, 100);
            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                attackingWarrior.Attack(defendingWarrior);
            }, "Warrior should not be able to attack when health is 30 or below!");

            Assert.AreEqual("Your HP is too low in order to attack other warriors!", ex.Message);
        }

        [TestCase(MIN_ATTACK_HP)]
        [TestCase(15)]
        [TestCase(0)]
        public void Test_Throw_Exception_If_Attacked_Warrior_Has_Too_Low_Health_To_Be_Attacked(int lowHealth)
        {
            var defendingWarrior = new Warrior("Olaf", 40, lowHealth);

            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                this.defaultWarrior.Attack(defendingWarrior);
            }, "Should not be able to attack warriors with low health!");

            Assert.AreEqual($"Enemy HP must be greater than {MIN_ATTACK_HP} in order to attack him!", ex.Message);
        }

        [Test]
        public void Test_Throw_Exception_If_Trying_To_Attack_Stronger_Warrior()
        {
            var defendingWarrior = new Warrior("Exodia", 999, 999);
            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                this.defaultWarrior.Attack(defendingWarrior);
            }, "Should not be able to attack stronger warriors!");

            Assert.AreEqual("You are trying to attack too strong enemy", ex.Message);
        }

        [Test]
        public void Test_Attack_Method_No_Kill_Works_Correctly()
        {
            var defendingWarrior = new Warrior("Olaf", 32, 100);
            int expectedDefendingWarriorHealth = defendingWarrior.HP - this.defaultWarrior.Damage;
            int expectedAttackingWarriorHealth = this.defaultWarrior.HP - defendingWarrior.Damage;

            this.defaultWarrior.Attack(defendingWarrior);
            int actualDefendingWarriorHealth = defendingWarrior.HP;
            int actualAttackingWarriorHealth = this.defaultWarrior.HP;

            Assert.AreEqual(expectedDefendingWarriorHealth, actualDefendingWarriorHealth);
            Assert.AreEqual(expectedAttackingWarriorHealth, actualAttackingWarriorHealth);
        }

        [Test]
        public void Test_Attack_Kill_Works_Correctly()
        {
            var defendingWarrior = new Warrior("Olaf", 35, 40);
            int expectedDefendingWarriorHealth = 0;

            this.defaultWarrior.Attack(defendingWarrior);
            int actualDefendingWarriorHealth = defendingWarrior.HP;

            Assert.AreEqual(expectedDefendingWarriorHealth, actualDefendingWarriorHealth);
        }

        [Test]
        public void Test_Attack_Should_Set_Defending_Warrior_Health_To_Zero()
        {
            var defendingWarrior = new Warrior("Olaf", 35, 31);
            int expectedDefendingWarriorHealth = 0;
            this.defaultWarrior.Attack(defendingWarrior);

            int actualDefendingWarriorHealth = defendingWarrior.HP;
            Assert.AreEqual(expectedDefendingWarriorHealth, actualDefendingWarriorHealth);
        }
    }
}