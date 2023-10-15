namespace Database.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class DatabaseTests
    {
        private Database data;
        private int capacity = 16;

        private Database emptyData;

        [SetUp]
        public void Setup()
        {
            this.data = new Database(new int[this.capacity]);
            this.emptyData = new Database();
        }

        [Test]
        public void Test_ConstructorShouldBeSetCorrectly()
        {
            Assert.AreEqual(this.data, this.data, "Database Array contains only integers.");
        }

        [Test]
        public void Test_DatabaseArrayShouldHaveCapacityOf16()
        {
            Assert.AreEqual(this.capacity, this.capacity, "Database Array Capacity should be 16.");
        }

        [Test]
        public void Test_DatabaseArrayShouldThrowExceptionIfCapacityIsNot16()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                if (this.emptyData.Count != this.capacity)
                {
                    throw new InvalidOperationException();
                }
            }, "Database Array should throw exception if capacity is not 16.");
        }

        [Test]
        public void Test_DatabaseArrayShouldAddElementAtTheNextFreeCell()
        {
            this.emptyData.Add(1);
            Assert.AreEqual(1, this.emptyData.Count, "Add method is not adding elements correctly.");
        }

        [Test]
        public void Test_DatabaseArrayShouldThrowExceptionIfWeTryToAdd17thElement()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                this.data.Add(17);
            }, "Database Array should throw exception if we try to add 17th element.");
        }

        [Test]
        public void Test_DatabaseArrayShouldRemoveElementAtTheLastIndex()
        {
            int initialCount = this.data.Count;
            this.data.Remove();
            Assert.AreEqual(initialCount - 1, this.data.Count, "Database Array is not removing element correctly.");
        }

        [Test]
        public void Test_DatabaseShouldThrowExceptionIfWeTryToRemoveElementFromEmptyDatabaseArray()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                this.emptyData.Remove();
            }, "Database Array should throw exception if we try to remove from empty database array.");
        }

        [Test]
        public void Test_DatabaseArrayFetchMethodShouldReturnAllElementsAsAnArray()
        {
            int[] expectedElements = { 1, 2, 3 };

            foreach (int element in expectedElements)
            {
                this.emptyData.Add(element);
            }

            int[] result = this.emptyData.Fetch();

            Assert.AreEqual(expectedElements.Length, result.Length, "Database Array Fetch method didn't return all elements as an array.");

            for (int i = 0; i < expectedElements.Length; i++)
            {
                Assert.AreEqual(expectedElements[i], result[i], "Element at {i} is incorrect.");
            }
        }
    }
}