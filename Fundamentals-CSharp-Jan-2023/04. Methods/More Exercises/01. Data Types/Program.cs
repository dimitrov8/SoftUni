using System;

namespace _01._Data_Types
{
    class Program
    {
        static void Main(string[] args)
        {
            string dataType = Console.ReadLine();

            FindDataTypeAndPrint(dataType);
        }

        private static void FindDataTypeAndPrint(string dataType)
        {
            if (dataType == "int")
            {
                int number = int.Parse(Console.ReadLine());
                Console.WriteLine(number * 2);
            }
            else if (dataType == "real")
            {
                double number = double.Parse(Console.ReadLine());
                Console.WriteLine($"{number * 1.5:F2}");
            }
            else if (dataType == "string")
            {
                string text = Console.ReadLine();
                Console.WriteLine($"${text}$");
            }
        }
    }
}