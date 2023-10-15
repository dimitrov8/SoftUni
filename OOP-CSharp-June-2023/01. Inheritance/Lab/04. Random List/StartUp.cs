using System;

namespace CustomRandomList
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            RandomList emptyRandomList = new RandomList();
            RandomList randomList = new RandomList { "1", "2", "3", "4" };

            Console.WriteLine(emptyRandomList.IsEmpty);
            emptyRandomList.RandomString();

            Console.WriteLine(randomList.IsEmpty);
            randomList.RandomString();
            Console.WriteLine($"Elements in the list: {string.Join(", ", randomList)}");
        }
    }
}