namespace DatabaseExtended.Tests
{
    using ExtendedDatabase;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class ExtendedDatabaseTests
    {
        private Database defaultDatabase;
        private Database fullDatabase;
        private readonly Person testPerson = new Person(899, "Mike");
        private const int CAPACITY = 16;

        [SetUp]
        public void SetUp()
        {
            this.defaultDatabase = new Database();
            this.fullDatabase = new Database(this.GetPeople());
        }

        [Test]
        public void Test_Constructor_Is_Set_Correct()
        {
            Assert.AreEqual(CAPACITY, this.fullDatabase.Count);
        }

        [Test]
        public void Test_Add_Method_Works_Correct()
        {
            this.defaultDatabase.Add(this.testPerson);
            Assert.AreEqual(1, this.defaultDatabase.Count);
        }

        [Test]
        public void Test_Throw_Exception_If_Trying_To_Add_Existing_Username()
        {
            this.defaultDatabase.Add(this.testPerson);
            Person personWithTheSameUsername = new Person(10, this.testPerson.UserName);

            Assert.That(() =>
            {
                this.defaultDatabase.Add(personWithTheSameUsername);
            }, Throws.InstanceOf<InvalidOperationException>().With.Message.EqualTo("There is already user with this username!"));
        }

        [Test]
        public void Test_Throw_Exception_If_Trying_To_Add_Existing_Id()
        {
            this.defaultDatabase = new Database(this.testPerson);
            Person personWithSameId = new Person(this.testPerson.Id, "George");

            Assert.That(() =>
            {
                this.defaultDatabase.Add(personWithSameId);
            }, Throws.InstanceOf<InvalidOperationException>().With.Message.EqualTo("There is already user with this Id!"));
        }

        [Test]
        public void Test_Throw_Exception_If_Trying_To_Exceed_The_Database_Capacity()
        {
            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                this.fullDatabase.Add(this.testPerson);
            });

            Assert.AreEqual($"Array's capacity must be exactly {CAPACITY} integers!", ex.Message);
        }

        [Test]
        public void Test_Throw_Exception_If_Add_Range_Method_Is_Exceeding_The_Database_Capacity()
        {
            Person[] people = new Person[17];
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                Database exceedingDatabase = new Database(people);
            });

            Assert.AreEqual("Provided data length should be in range [0..16]!", ex.Message);
        }

        [Test]
        public void Test_Remove_Method_Is_Working_Correctly()
        {
            this.defaultDatabase = new Database(new Person(456, "testUsername1"), new Person(567, "testUsername2"));
            int expected = this.defaultDatabase.Count - 1;
            this.defaultDatabase.Remove();
            int actual = this.defaultDatabase.Count;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Test_Throw_Exception_If_Trying_To_Remove_From_Empty_Database()
        {
            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                this.defaultDatabase.Remove();
            });

            Assert.IsInstanceOf<InvalidOperationException>(ex);
        }

        [TestCase("nonExistingUsername")]
        public void Test_Throw_Exception_If_Find_By_Method_Does_Not_Find_Username(string nonExistingUsername)
        {
            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                this.defaultDatabase.FindByUsername(nonExistingUsername);
            });

            Assert.AreEqual("No user is present by this username!", ex.Message);
        }

        [TestCase(null)]
        [TestCase("")]
        public void Test_Throw_Exception_If_Find_By_Method_Username_Is_Null_Or_Empty(string nullOrEmptyUsername)
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                this.fullDatabase.FindByUsername(nullOrEmptyUsername);
            });

            Assert.AreEqual("Value cannot be null. (Parameter 'Username parameter is null!')", ex.Message);
        }

        [Test]
        public void Test_Find_By_Username_Works_Correct()
        {
            this.defaultDatabase = new Database(this.testPerson);
            Person expected = this.testPerson;
            Person actual = this.defaultDatabase.FindByUsername(this.testPerson.UserName);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Test_Find_By_Id_Works_Correct()
        {
            this.defaultDatabase = new Database(this.testPerson);
            Person expected = this.testPerson;
            Person actual = this.defaultDatabase.FindById(this.testPerson.Id);

            Assert.AreEqual(expected, actual);
        }

        [TestCase(200)]
        public void Test_Throw_Exception_If_Trying_To_Find_By_Id_That_Does_Not_Exist(long nonExistingId)
        {
            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                this.defaultDatabase.FindById(nonExistingId);
            });

            Assert.AreEqual("No user is present by this ID!", ex.Message);
        }

        [TestCase(-1)]
        public void Test_Throw_Exception_If_Trying_To_Find_By_Id_That_Is_Negative(long negativeId)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                this.defaultDatabase.FindById(negativeId);
            });
        }

        private Person[] GetPeople()
        {
            Person[] people = new Person[CAPACITY];

            for (int i = 0; i < CAPACITY; i++)
            {
                people[i] = new Person(i + 11, i.ToString());
            }

            return people;
        }
    }
}