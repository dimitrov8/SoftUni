namespace Book.Tests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    public class Tests
    {
        private Book defaultBook;
        private Dictionary<int, string> footnote;

        [SetUp]
        public void SetUp()
        {
            this.defaultBook = new Book("Best Book", "Best Author");
            this.footnote = new Dictionary<int, string>();
        }


        [Test]
        public void Test_Constructor_Works_Correct()
        {
            Assert.AreEqual("Best Book", this.defaultBook.BookName);
            Assert.AreEqual("Best Author", this.defaultBook.Author);
            Assert.IsNotNull(this.footnote);
        }

        [TestCase(null)]
        [TestCase("")]
        public void Test_Throw_Exception_If_Book_Name_Null_Or_Empty(string invalidBookName)
        {
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                this.defaultBook = new Book(invalidBookName, this.defaultBook.Author);
            });

            Assert.AreEqual($"Invalid {nameof(this.defaultBook.BookName)}!", ex.Message);
        }

        [TestCase(null)]
        [TestCase("")]
        public void Test_Throw_Exception_If_Book_Author_Null_Or_Empty(string invalidAuthorName)
        {
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                this.defaultBook = new Book(this.defaultBook.BookName, invalidAuthorName);
            });

            Assert.AreEqual($"Invalid {nameof(this.defaultBook.Author)}!", ex.Message);
        }

        [Test]
        public void Test_Throw_Exception_If_Trying_To_Add_Existing_Key_To_Footnote()
        {
            this.defaultBook.AddFootnote(1, "test");

            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                this.defaultBook.AddFootnote(1, "test");
            });

            Assert.AreEqual("Footnote already exists!", ex.Message);
        }

        [Test]
        public void Test_Footnote_Works_Correct()
        {
            this.footnote.Add(1, "test");

            Assert.AreEqual(1, this.footnote.Count);
        }

        [Test]
        public void Test_Throw_Exception_If_Footnote_Doesnt_Exist()
        {
            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                this.defaultBook.FindFootnote(5);
            });

            Assert.AreEqual("Footnote doesn't exists!", ex.Message);
        }

        [Test]
        public void Test_FindFootnote_Works_Correct()
        {
            this.defaultBook.AddFootnote(1, "test");

            Assert.AreEqual("Footnote #1: test", this.defaultBook.FindFootnote(1));
        }

        [Test]
        public void Test_Throw_Exception_If_Trying_To_Change_Value_Of_Not_Existing_Key()
        {
            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                this.defaultBook.AlterFootnote(1, "newText");
            });

            Assert.AreEqual("Footnote does not exists!", ex.Message);
        }

        [Test]
        public void Test_Change_Text_Of_Existing_Key_Works_Correct()
        {
            this.defaultBook.AddFootnote(1, "test");

            this.defaultBook.AlterFootnote(1, "newText");

            Assert.AreEqual("Footnote #1: newText", this.defaultBook.FindFootnote(1));
        }
    }
}