using System;

namespace GenericBoxOfInteger
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            int nOfIntegers = int.Parse(Console.ReadLine());
            for (int i = 0; i < nOfIntegers; i++)
            {
                Console.WriteLine(new Box<int>(int.Parse(Console.ReadLine())));
            }
        }
    }
}