namespace Common_Elements
{
    using System;
    using System.Linq;
    class Program
    {
        static void Main(string[] args)
        {
            string[] arr1 = Console.ReadLine()
                .Split(" ")
                .ToArray();
            string[] arr2 = Console.ReadLine()
               .Split(" ")
               .ToArray();

            arr1 = arr2.Intersect(arr1).ToArray();
            Console.WriteLine(string.Join(" ", arr1));
        }
    }
}