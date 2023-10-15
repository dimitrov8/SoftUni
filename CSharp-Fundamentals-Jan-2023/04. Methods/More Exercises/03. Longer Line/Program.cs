using System;

namespace _03._Longer_Line
{
    class Program
    {
        static void Main(string[] args)
        {
            double x1 = double.Parse(Console.ReadLine());
            double y1 = double.Parse(Console.ReadLine());
            double x2 = double.Parse(Console.ReadLine());
            double y2 = double.Parse(Console.ReadLine());

            double x3 = double.Parse(Console.ReadLine());
            double y3 = double.Parse(Console.ReadLine());
            double x4 = double.Parse(Console.ReadLine());
            double y4 = double.Parse(Console.ReadLine());

            // The first and the second pair of points form two different lines
            // We have to print the longer one 
            // So we call a method to calculate both pairs of points
            double firstLine = CalculateDistance(x1, y1, x2, y2);
            double secondLine = CalculateDistance(x3, y3, x4, y4);

            // We have to print the longer line
            if (firstLine >= secondLine) // If firstLine is longer or equal to secondLine
            {
                // Check which pair of points is closer
                // And start printing from it
                Console.WriteLine(CalculateDistance(x1, y1) <= CalculateDistance(x2, y2)
                    ? $"({x1}, {y1})({x2}, {y2})"
                    : $"({x2}, {y2})({x1}, {y1})");
            }
            else // If secondLine is longer than the first line
            {
                // Check which pair of points is closer
                // And start printing from it
                Console.WriteLine(CalculateDistance(x3, y3) <= CalculateDistance(x4, y4)
                    ? $"({x3}, {y3})({x4}, {y4})"
                    : $"({x4}, {y4})({x3}, {y3})");
            }
        }

        // x2 and y2 are the center of the coordinate system
        // x2 and y2 are equal to 0d because that's our center of the coordinate system
        static double CalculateDistance(double x1, double y1, double x2 = 0d, double y2 = 0d)
        {
            // Use the Pythagorean theorem to find the distance of two points
            return Math.Sqrt(Math.Pow((x1 - x2), 2) + Math.Pow((y1 - y2), 2));
            // 34 - 5 = -29²(Math.Pow) = 841
            // (-3) - 9 = (-12)² (Math.Pow) = 144
            // 841 + 144 = √985(Math.Sqrt) = 31. 38..
        }
    }
}