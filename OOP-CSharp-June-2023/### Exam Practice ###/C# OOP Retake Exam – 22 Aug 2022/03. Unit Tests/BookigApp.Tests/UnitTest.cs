namespace BookigApp.Tests
{
    using FrontDeskApp;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public class Tests
    {
        private string fullName;
        private int category;
        private List<Room> rooms;
        private List<Booking> bookings;

        [SetUp]
        public void Setup()
        {
            this.fullName = "The Best Hotel";
            this.category = 5;
            this.rooms = new List<Room>();
            this.bookings = new List<Booking>();
        }

        [Test]
        public void Test_Hotel_Constructor_Works_Correct()
        {
            var hotel = new Hotel(this.fullName, this.category);

            Assert.AreEqual("The Best Hotel", hotel.FullName);
            Assert.AreEqual(5, hotel.Category);
            Assert.AreEqual(0, hotel.Turnover);
            Assert.NotNull(this.rooms);
            Assert.NotNull(this.bookings);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Test_Hotel_Name_Cannot_Be_Null_Or_WhiteSpace(string invalidHotelName)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var hotel = new Hotel(invalidHotelName, this.category);
            });
        }

        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(6)]
        public void Test_Hotel_Category_Cannot_Be_Zero_Negative_Or_More_Than_Five(int invalidCategory)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var hotel = new Hotel(this.fullName, invalidCategory);
            });
        }

        [Test]
        public void Test_Hotel_Add_Room_Works_Correct()
        {
            var hotel = new Hotel(this.fullName, this.category);
            var room = new Room(2, 20);
            hotel.AddRoom(room);
            
            Assert.AreEqual(1, hotel.Rooms.Count);
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void Test_Throw_Exception_If_Trying_To_Book_For_0_Or_Negative_Value_Adults(int invalidAdultsCount)
        {
            var hotel = new Hotel(this.fullName, this.category);
            var room = new Room(2, 20);
            hotel.AddRoom(room);
            
            Assert.Throws<ArgumentException>(() =>
            {
                hotel.BookRoom(invalidAdultsCount, 0, 1, 100);
            });
        }
        
        [TestCase(-1)]
        public void Test_Throw_Exception_If_Trying_To_Book_For_Negative_Value_Children(int invalidChildrenCount)
        {
            var hotel = new Hotel(this.fullName, this.category);
            var room = new Room(2, 20);
            hotel.AddRoom(room);
            
            Assert.Throws<ArgumentException>(() =>
            {
                hotel.BookRoom(1, invalidChildrenCount, 1, 100);
            });
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void Throw_Exception_If_Trying_To_Stay_For_Zero_Or_Lower_Days(int invalidDuration)
        {
            var hotel = new Hotel(this.fullName, this.category);
            var room = new Room(2, 20);
            hotel.AddRoom(room);

            Assert.Throws<ArgumentException>(() =>
            {
                hotel.BookRoom(2, 0, invalidDuration, 100);
            });
        }

        [Test]
        public void Test_Cannot_Book_If_Not_Enough_Beds()
        {
            var hotel = new Hotel(this.fullName, this.category);
            var room = new Room(2, 20);
            hotel.AddRoom(room);

            hotel.BookRoom(4, 0, 2, 100);
            
            Assert.AreEqual(0, hotel.Turnover);
        }

        [Test]
        public void Test_Can_Book_Room()
        {
            var hotel = new Hotel(this.fullName, this.category);
            var room = new Room(2, 20);
            hotel.AddRoom(room);
            
            hotel.BookRoom(2, 0, 1, 20);
            
            Assert.AreEqual(20, hotel.Turnover);
            Assert.AreEqual(1, hotel.Bookings.Count);
            Assert.AreEqual(1, hotel.Rooms.Count);
        }

        [Test]
        public void Test_Cannot_Book_If_Budget_Is_Low()
        {
            var hotel = new Hotel(this.fullName, this.category);
            var room = new Room(2, 20);
            hotel.AddRoom(room);
            
            hotel.BookRoom(2, 0, 1, 10);
            
            Assert.AreEqual(0, hotel.Turnover);
            Assert.AreEqual(0, hotel.Bookings.Count);
            Assert.AreEqual(1, hotel.Rooms.Count);
        }
    }
}