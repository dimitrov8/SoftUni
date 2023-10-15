namespace SumOfIntegers
{
    using System;

    public class StartUp
    {
        static void Main(string[] args)
        {
            string[] input = Console.ReadLine()
                .Split();

            int sum = 0;
            foreach (var element in input)
            {
                try
                {
                    if (int.TryParse(element, out int parsedElement))
                    {
                        sum += parsedElement;
                        PrintProcessedElement(element, sum);
                    }

                    else if (long.TryParse(element, out _))
                        throw new OverflowException($"The element '{element}' is out of range!");

                    else
                        throw new FormatException($"The element '{element}' is in wrong format!");
                }
                catch (FormatException fex)
                {
                    Console.WriteLine(fex.Message);
                    PrintProcessedElement(element, sum);
                }
                catch (OverflowException ofex)
                {
                    Console.WriteLine(ofex.Message);
                    PrintProcessedElement(element, sum);
                }
            }

            Console.WriteLine($"The total sum of all integers is: {sum}");
        }

        private static void PrintProcessedElement(string element, int sum)
            => Console.WriteLine($"Element '{element}' processed - current sum: {sum}");
    }
}