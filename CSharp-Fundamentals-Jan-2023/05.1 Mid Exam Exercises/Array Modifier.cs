using System;
using System.Linq;

namespace Array_Modifier
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            string command;
            while ((command = Console.ReadLine()) != "end")
            {
                string[] cmdArgs = command.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string type = cmdArgs[0];

                if (type == "swap" || type == "multiply")
                {
                    int index1 = int.Parse(cmdArgs[1]);
                    int index2 = int.Parse(cmdArgs[2]);
                    if (type == "swap")
                    {
                        (arr[index1], arr[index2]) = (arr[index2], arr[index1]);
                        // // Create temp to store the value
                        // int temp = arr[index1];
                        //
                        // arr[index1] = arr[index2];
                        // arr[index2] = temp;
                    }

                    else if (type == "multiply")
                    {
                        arr[index1] *= arr[index2];
                    }
                }
                else if (type == "decrease")
                {
                    for (int i = 0; i < arr.Length; i++)
                    {
                        arr[i] -= 1;
                    }
                }
            }

            Console.WriteLine(string.Join(", ", arr));
        }
    }
}