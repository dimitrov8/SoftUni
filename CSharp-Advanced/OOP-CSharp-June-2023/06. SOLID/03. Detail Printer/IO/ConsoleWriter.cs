namespace P03.Detail_Printer.IO
{
    using Contracts;
    using System;

    public class ConsoleWriter : IWriter
    {
        public void Write(string text) => Console.Write(text);

        public void WriteLine(string text) => Console.WriteLine(text);
    }
}