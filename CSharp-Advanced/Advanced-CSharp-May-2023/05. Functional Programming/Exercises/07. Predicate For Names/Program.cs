using System;
using System.Linq;

namespace _07._Predicate_For_Names
{
    class Program
    {
        static void Main(string[] args)
        {
            int nameLength = int.Parse(Console.ReadLine());
            string[] names = Console.ReadLine().Split();

            Predicate<string> isDesiredLength = n => n.Length <= nameLength;
            Console.WriteLine(string.Join(Environment.NewLine, names.Where(n => isDesiredLength(n))));
        }
    }
}