using System;
using System.Collections.Generic;
using System.Linq;

namespace WarmWinter
{
    class Program
    {
        static void Main(string[] args)
        {
            var hats = new Stack<int>(Console.ReadLine()!
                .Split()
                .Select(int.Parse));

            var scarfs = new Queue<int>(Console.ReadLine()!
                .Split()
                .Select(int.Parse));

            var sets = new Queue<int>();
            
            while (true)
            {
                if (hats.Peek() == scarfs.Peek())
                {
                    scarfs.Dequeue();
                    hats.Push(hats.Pop() + 1);
                }
                else if (hats.Peek() < scarfs.Peek())
                {
                    hats.Pop();
                }
                else if (hats.Peek() > scarfs.Peek())
                {
                    sets.Enqueue(hats.Pop() + scarfs.Dequeue());
                }

                if (!hats.Any() || !scarfs.Any())
                {
                    break;
                }
            }

            Console.WriteLine($"The most expensive set is: {sets.Max()}");
            Console.WriteLine(string.Join(" ", sets));
        }
    }
}
