﻿namespace WildFarm.IO
{
    using Interfaces;
    using System;

    public class ConsoleReader : IReader
    {
        public string ReadLine() => Console.ReadLine();
    }
}