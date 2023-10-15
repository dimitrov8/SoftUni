namespace Skeleton.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class AxeTests
    {
        private Axe axe;
        private readonly int attack = 6;
        private readonly int durability = 5;

        private Dummy dummy;
        private readonly int health = 7;
        private readonly int experience = 100;

        [SetUp]
        public void Setup()
        {
            this.axe = new Axe(this.attack, this.durability);
            this.dummy = new Dummy(this.health, this.experience);
        }

        [Test]
        public void Test_AxeShouldLoseDurabilityAfterAttack()
        {
            this.axe.Attack(this.dummy);

            Assert.That(this.axe.DurabilityPoints, Is.EqualTo(this.durability - 1)
                , "Durability points didn't change after attack.");
        }

        [Test]
        public void Test_AxeShouldThrowExceptionWhenDurabilityIsZeroOrBelow()
        {
            this.axe = new Axe(10, -5);

            Assert.Throws<InvalidOperationException>(() =>
                this.axe.Attack(this.dummy), "Axe cannot attack when durability is zero or below.");
        }
    }
}