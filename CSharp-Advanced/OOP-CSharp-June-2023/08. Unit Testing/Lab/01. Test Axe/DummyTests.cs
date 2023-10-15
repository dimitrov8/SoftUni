namespace Skeleton.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class DummyTests
    {
        private Dummy dummy;
        private readonly int health = 30;
        private readonly int experience = 100;

        private Dummy deadDummy;

        [SetUp]
        public void Setup()
        {
            this.dummy = new Dummy(this.health, this.experience);
            this.deadDummy = new Dummy(-5, 100);
        }

        [Test]
        public void Test_DummyShouldLoseHealthWhenAttacked()
        {
            int attackPoints = 3;
            this.dummy.TakeAttack(attackPoints);

            Assert.That(this.dummy.Health, Is.EqualTo(this.health - attackPoints), "Dummy cannot lose health when attacked.");
        }

        [Test]
        public void Test_DeadDummyShouldThrowExceptionWhenAttacked()
        {
            Assert.Throws<InvalidOperationException>(() =>
                this.deadDummy.TakeAttack(3), "Dead dummy should throw invalid operation exception.");
        }

        [Test]
        public void Test_DeadDummyShouldGiveExperience()
        {
            Assert.That(this.deadDummy.GiveExperience(), Is.EqualTo(this.experience), "Dead dummy is not returning experience value correctly.");
        }

        [Test]
        public void Test_DummyCannotGiveExperience()
        {
            Assert.Throws<InvalidOperationException>(() =>
                this.dummy.GiveExperience(), "Alive dummy should not be returning experience.");
        }
    }
}