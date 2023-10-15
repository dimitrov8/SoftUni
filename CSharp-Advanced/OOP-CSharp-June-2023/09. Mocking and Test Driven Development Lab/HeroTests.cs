using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skeleton.Tests
{
    [TestFixture]
    public class HeroTests
    {
        [Test]
        public void TestHeroGainsXPWhenTargetDies()
        {
            // Arrange
            Mock<ITarget> targetMock = new Mock<ITarget>();
            targetMock.Setup(t => t.GiveExperience()).Returns(100);
            targetMock.Setup(t => t.IsDead()).Returns(true);

            Mock<IWeapon> weaponMock = new Mock<IWeapon>();
            weaponMock.Setup(w => w.Attack(targetMock.Object));

            // Pass null as the first argument, and a fake weapon as the second argument
            Hero hero = new Hero(null, weaponMock.Object);

            // Act
            hero.Attack(targetMock.Object);

            // Assert
            Assert.AreEqual(100, hero.Experience);
        }
    }
}

