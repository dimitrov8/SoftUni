namespace Balanced_Brackets
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            int rows = int.Parse(Console.ReadLine());

            int openingCount = 0;
            int closingCount = 0;

            for (int i = 0; i < rows; i++)
            {
                string input = Console.ReadLine();

                if (input == "(")
                {
                    openingCount++;
                }
                else if (input == ")")
                {
                    closingCount++;
                }
                if (openingCount - closingCount == 2 || closingCount > openingCount)
                {
                    break;
                }
            }
            if (openingCount != closingCount)
            {
                Console.WriteLine("UNBALANCED");
            }
            else
            {
                Console.WriteLine("BALANCED");
            }
        }
    }
}