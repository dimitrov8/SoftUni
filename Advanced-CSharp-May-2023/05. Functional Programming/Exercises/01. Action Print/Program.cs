using System;

namespace _01._Action_Print
{
    class Program
    {
        static void Main(string[] args)
        {
            Action<string[]> print = x => Console.WriteLine(string.Join(Environment.NewLine, x));

            print(Console.ReadLine().Split(' '));
        }
    }
}