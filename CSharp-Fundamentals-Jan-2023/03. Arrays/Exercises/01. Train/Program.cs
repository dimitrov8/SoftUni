namespace Train
{
    using System;
    using System.Linq;
    class Program
    {
        static void Main(string[] args)
        {
            int wagons = int.Parse(Console.ReadLine());

            int[] arr = new int[wagons];

            int people = 0;
            for (int i = 0; i < wagons; i++)
            {
                arr[i] += people = int.Parse(Console.ReadLine());
            }
            Console.WriteLine(string.Join(" ", arr));
            Console.WriteLine(arr.Sum());
        }
    }
}
