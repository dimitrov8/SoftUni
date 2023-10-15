using System;

namespace GenericBoxOfString
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            int nOfStrings = int.Parse(Console.ReadLine());
            for (int i = 0; i < nOfStrings; i++)
            {
                Console.WriteLine(new Box<string>(Console.ReadLine()));
            }
        }
    }
}