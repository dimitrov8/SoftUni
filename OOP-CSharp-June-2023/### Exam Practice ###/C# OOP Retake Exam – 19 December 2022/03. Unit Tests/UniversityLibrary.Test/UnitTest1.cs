namespace UniversityLibrary.Test
{
    using NUnit.Framework;
    using System.Linq;
    using System.Text;

    public class Tests
    {
        private TextBook defaultBook;
        private UniversityLibrary defaultLibrary;

        [SetUp]
        public void Setup()
        {
            this.defaultBook = new TextBook("The best book", "The best author", "The best category");
            this.defaultLibrary = new UniversityLibrary();
        }

        [Test]
        public void Test_Constructor_Works()
        {
            Assert.IsInstanceOf<UniversityLibrary>(this.defaultLibrary);
            Assert.IsNotNull(this.defaultLibrary);
        }

        [Test]
        public void Test_Add_Book_Works()
        {
            this.defaultLibrary.AddTextBookToLibrary(this.defaultBook);

            Assert.AreEqual(1, this.defaultLibrary.Catalogue.Count);
        }

        [Test]
        public void Test_Inventory_Number_Works_Correct()
        {
            var goodBook = new TextBook("Good book", "Good author", "Good category");
            this.defaultLibrary.AddTextBookToLibrary(this.defaultBook);
            this.defaultLibrary.AddTextBookToLibrary(goodBook);

            Assert.AreEqual(2, goodBook.InventoryNumber);
        }

        [Test]
        public void Test_Holder_Is_Set_Correct()
        {
            var goodBook = new TextBook("Good book", "Good author", "Good category");
            this.defaultLibrary.AddTextBookToLibrary(goodBook);

            string expectedResult = "Good book loaned to Sett.";
            string actualResult = this.defaultLibrary.LoanTextBook(1, "Sett");

            Assert.AreEqual("Sett", goodBook.Holder);
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void Test_Book_Is_Not_Returned()
        {
            var goodBook = new TextBook("Good book", "Good author", "Good category");
            this.defaultLibrary.AddTextBookToLibrary(goodBook);
            this.defaultLibrary.LoanTextBook(1, "Sett");

            string expectedResult = "Sett still hasn't returned Good book!";
            string actualResult = this.defaultLibrary.LoanTextBook(1, "Sett");

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void Test_Return_Book_Removes_The_Book()
        {
            var goodBook = new TextBook("Good book", "Good author", "Good category");
            this.defaultLibrary.AddTextBookToLibrary(goodBook);
            this.defaultLibrary.AddTextBookToLibrary(this.defaultBook);
            goodBook.Holder = "Sett";

            Assert.AreEqual("Good book is returned to the library.", this.defaultLibrary.ReturnTextBook(1));
            Assert.AreEqual(string.Empty, goodBook.Holder);
            Assert.AreEqual(2, this.defaultLibrary.Catalogue.Count);
        }

        [Test]
        public void Test_Text_Book_Constructor_Works_Correct()
        {
            var book = new TextBook("Good book", "Good author", "Good category");
            Assert.IsInstanceOf<TextBook>(book);
            Assert.AreEqual("Good book", book.Title);
            Assert.AreEqual("Good author", book.Author);
            Assert.AreEqual("Good category", book.Category);

            Assert.AreEqual(0, book.InventoryNumber);
        }

        [Test]
        public void Test_Book_Exist_In_The_Library()
        {
            var goodBook = new TextBook("Good book", "Good author", "Good category");
            this.defaultLibrary.AddTextBookToLibrary(goodBook);

            var actualResult = this.defaultLibrary.Catalogue.FirstOrDefault(b => b.InventoryNumber == 1);
            Assert.AreEqual(goodBook, actualResult);
        }

        [Test]
        public void Test_Book_To_String_Method_Works()
        {
            var goodBook = new TextBook("Good book", "Good author", "Good category");

            var expectedResultSb = new StringBuilder();
            expectedResultSb.AppendLine("Book: Good book - 1");
            expectedResultSb.AppendLine("Category: Good category");
            expectedResultSb.AppendLine("Author: Good author");
            string expectedResult = expectedResultSb.ToString().TrimEnd();

            string actualResult = this.defaultLibrary.AddTextBookToLibrary(goodBook);

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
