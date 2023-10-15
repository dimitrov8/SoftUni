namespace ArrayRotation
{
    using System;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            // Recieve an array
            int[] arr = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            // Rotations
            int rotations = int.Parse(Console.ReadLine());

            for (int rot = 1; rot <= rotations; rot++)
            {
                int fistElement = arr[0];
                for (int i = 1; i < arr.Length; i++)
                {
                    // Element from[1] goes to [0];
                    // Elemet from [2] goes to [1];
                    //.... 
                    arr[i - 1] = arr[i];
                }
                // Put the lastElement[Index] = firsElement
                arr[arr.Length - 1] = fistElement;
            }
            Console.WriteLine(string.Join(" ", arr));
        }
    }
}