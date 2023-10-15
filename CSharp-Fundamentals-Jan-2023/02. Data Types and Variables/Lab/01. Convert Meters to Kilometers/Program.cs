namespace Convert_Meters_to_Kilometers
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            int input = int.Parse(Console.ReadLine());

            Console.WriteLine($"{input / 1000.0:f2}");
        }
    }
}
