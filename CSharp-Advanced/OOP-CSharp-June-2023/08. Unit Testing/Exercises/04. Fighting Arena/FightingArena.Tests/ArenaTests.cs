namespace FightingArena.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class ArenaTests
    {
        private Arena arena;

        [SetUp]
        public void SetUp()
        {
            this.arena = new Arena();
        }

        [Test]
        public void Test_Constructor_Initialize_Warriors_Collection()
        {
            this.arena = new Arena();

            Assert.IsNotNull(this.arena);
        }

        [Test]
        public void Test_Arena_Constructor_Works_Correctly()
        {
            int expectedCount = 0;
            int actualCount = this.arena.Count;
            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void Test_Arena_Enrolled_Warriors_Count_Works_Correctly()
        {
            var w1 = new Warrior("Draven", 40, 100);
            var w2 = new Warrior("Olaf", 50, 100);

            this.arena.Enroll(w1);
            this.arena.Enroll(w2);

            int expectedCount = 2;
            int actualCount = this.arena.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void Test_Throw_Exception_If_Trying_To_Enroll_Existing_Warrior()
        {
            var warrior = new Warrior("Draven", 40, 100);

            this.arena.Enroll(warrior);
            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                this.arena.Enroll(warrior);
            }, "Should not enroll existing warrior!");

            Assert.AreEqual("Warrior is already enrolled for the fights!", ex.Message);
        }

        [Test]
        public void Test_Throw_Exception_If_Trying_To_Enroll_Existing_Warrior_Name()
        {
            var warrior = new Warrior("Draven", 40, 100);

            this.arena.Enroll(warrior);
            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                this.arena.Enroll(new Warrior("Draven", 45, 55));
            }, "Should not enroll warriors with the same name!");

            Assert.AreEqual("Warrior is already enrolled for the fights!", ex.Message);
        }

        [Test]
        public void Test_Throw_Exception_If_Trying_To_Fight_With_Missing_Defender()
        {
            this.arena.Enroll(new Warrior("Draven", 40, 100));
            string nonExistingDefenderName = "Sett";

            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                this.arena.Fight("Draven", nonExistingDefenderName);
            }, "Should not fight with warriors that are not enrolled!");

            Assert.AreEqual($"There is no fighter with name {nonExistingDefenderName} enrolled for the fights!", ex.Message);
        }

        [Test]
        public void Test_Throw_Exception_If_Trying_To_Fight_With_Missing_Attacker()
        {
            this.arena.Enroll(new Warrior("Draven", 40, 100));
            string nonExistingAttacker = "Sett";

            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                this.arena.Fight(nonExistingAttacker, "Draven");
            }, "Should not fight with warriors that are not enrolled!");

            Assert.AreEqual($"There is no fighter with name {nonExistingAttacker} enrolled for the fights!", ex.Message);
        }

        [Test]
        public void Test_Fight_Method_Works_Correctly()
        {
            var attacker = new Warrior("Draven", 40, 100);
            var defender = new Warrior("Darius", 32, 80);
            this.arena.Enroll(attacker);
            this.arena.Enroll(defender);

            int expectedAttackerHealth = attacker.HP - defender.Damage;
            int expectedDefenderHealth = defender.HP - attacker.Damage;

            this.arena.Fight(attacker.Name, defender.Name);

            int actualAttackerHealth = attacker.HP;
            int actualDefenderHealth = defender.HP;

            Assert.AreEqual(expectedAttackerHealth, actualAttackerHealth);
            Assert.AreEqual(expectedDefenderHealth, actualDefenderHealth);
        }
    }
}