namespace WildFarm.IO
{
    using Interfaces;
    using System;

    public class ConsoleWriter : IWriter
    {
        public void Write(string text) => Console.WriteLine(text);

        public void WriteLine(string text) => Console.WriteLine(text);
    }
}