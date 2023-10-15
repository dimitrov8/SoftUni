using System;

namespace _06._Middle_Characters
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();

            GetMiddleChars(input);
        }

        static void GetMiddleChars(string input)
        {
            if (input.Length % 2 != 0)
            {
                int index = input.Length / 2;
                char letter = input[index];
                Console.WriteLine(letter);
            }
            else if (input.Length % 2 == 0)
            {
                int index1 = (input.Length - 1) / 2;
                int index2 = index1 + 1;
                char[] letters = new char[2] {input[index1], input[index2]};
                Console.WriteLine(string.Join("", letters));
            }
        }
    }
}