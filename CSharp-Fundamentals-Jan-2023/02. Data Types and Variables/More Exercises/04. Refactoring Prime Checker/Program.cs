namespace Refactoring_Prime_Checker
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            for (int currentN = 2; currentN <= n; currentN++)
            {
                bool isPrime = true;
                for (int currentDivideN = 2; currentDivideN < currentN; currentDivideN++)
                {
                    if (currentN % currentDivideN == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                Console.WriteLine($"{currentN} -> {isPrime.ToString().ToLower()}");
            }
        }
    }
}
