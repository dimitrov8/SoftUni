using System;
using System.Collections.Generic;
using System.Linq;

namespace Scheduling
{
    class Program
    {
        static void Main(string[] args)
        {
            Stack<int> threadValue = new Stack<int>(Console.ReadLine()!
                .Split(", ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse));

            Queue<int> threads = new Queue<int>(Console.ReadLine()!
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse));

            int valueOfTheTaskToBeTerminated = int.Parse(Console.ReadLine()!);

            while (true)
            {
                if (threadValue.Peek() <= threads.Peek())
                {
                    threadValue.Pop();
                    threads.Dequeue();
                }
                else if (threadValue.Peek() > threads.Peek())
                {
                    threads.Dequeue();
                }

                if (threadValue.Peek() == valueOfTheTaskToBeTerminated)
                {
                    break;
                }

            }
            Console.WriteLine($"Thread with value {threads.Peek()} killed task {threadValue.Pop()}");

            while (threads.Any())
            {
                Console.Write($"{threads.Dequeue()} ");
            }
        }
    }
}