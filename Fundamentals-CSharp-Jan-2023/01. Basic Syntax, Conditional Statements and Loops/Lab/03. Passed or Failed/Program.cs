namespace Passed_or_Failed
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            double input = double.Parse(Console.ReadLine());

            string output = "Failed!";
            if (input >= 3.00)
            {
                output = "Passed!";
            }
            Console.WriteLine(output);
        }
    }
}
