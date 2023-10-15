using System;

namespace Elevator
{
    class Program
    {
        static void Main(string[] args)
        {
            int numberOfPeople = int.Parse(Console.ReadLine());
            int capacity = int.Parse(Console.ReadLine());

            // Calculate how many courses will be needed
            int courses = (int)Math.Ceiling((double)numberOfPeople / capacity);

            Console.WriteLine(courses);
        }
    }
}
