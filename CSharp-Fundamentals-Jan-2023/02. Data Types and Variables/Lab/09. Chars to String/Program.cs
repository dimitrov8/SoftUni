using System;

namespace Char_To_String
{
    class Program
    {
        static void Main(string[] args)
        {
            char char1 = char.Parse(Console.ReadLine());
            char char2 = char.Parse(Console.ReadLine());
            char char3 = char.Parse(Console.ReadLine());

            string text = "" + char1 + char2 + char3;

            Console.WriteLine($"{text}");
        }
    }
}
