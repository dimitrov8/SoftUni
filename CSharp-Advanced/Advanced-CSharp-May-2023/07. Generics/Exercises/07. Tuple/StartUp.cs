using System;

namespace Tuple
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            string[] personNameAndAddress = Console.ReadLine()!.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            int addressCount = personNameAndAddress.Length - 2; // Used to determinate the numbers of elements in the address that we should join
            CustomTuple<string, string> info = new CustomTuple<string, string>(personNameAndAddress[0] + " " + personNameAndAddress[1], 
                string.Join(" ", personNameAndAddress, 2, addressCount)); // Join the address by starting at index 2 and take all the elements from the address

            string[] personBeer = Console.ReadLine()!.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            CustomTuple<string, int> beer = new CustomTuple<string, int>(personBeer[0], int.Parse(personBeer[1]));

            string[] numbers = Console.ReadLine()!.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            CustomTuple<int, double> num = new CustomTuple<int, double>(int.Parse(numbers[0]), double.Parse(numbers[1]));

            Console.WriteLine(info);
            Console.WriteLine(beer);
            Console.WriteLine(num);
        }
    }
}