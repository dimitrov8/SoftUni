namespace _06._Calculate_Rectangle_Area
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            double width = double.Parse(Console.ReadLine());
            double height = double.Parse(Console.ReadLine());

            double area = GetAreaOfRectangle(width, height);
            Console.WriteLine(area);
        }

        static double GetAreaOfRectangle(double width, double height)
        {
            return width * height;
        }
    }
}