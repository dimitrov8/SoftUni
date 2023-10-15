namespace P03.Detail_Printer.IO
{
    using Contracts;
    using System;

    public class ConsoleReader : IReader
    {
        public string ReadLine() => Console.ReadLine();
    }
}