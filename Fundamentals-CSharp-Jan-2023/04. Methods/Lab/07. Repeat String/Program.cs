namespace _07._Repeat_String
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            int nTimes = int.Parse(Console.ReadLine());

            RepeatString(input , nTimes);
        }

        private static void RepeatString(string input, int nTImes)
        {
            for (int i = 1; i <= nTImes; i++)
            {
                Console.Write(input);
            }
        }
    }
}