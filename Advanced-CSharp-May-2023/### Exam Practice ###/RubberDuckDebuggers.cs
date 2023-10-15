using System;
using System.Collections.Generic;
using System.Linq;

namespace RubberDuckDebuggers
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = new Dictionary<string, int>
            {
                { "Darth Vader Ducky", 0 },
                { "Thor Ducky", 0 },
                { "Big Blue Rubber Ducky", 0 },
                { "Small Yellow Rubber Ducky", 0 }
            };

            var taskTime = new Queue<int>(Console.ReadLine()!
                .Split()
                .Select(int.Parse));

            var numberOfTasks = new Stack<int>(Console.ReadLine()!
                .Split()
                .Select(int.Parse));

            while (taskTime.Any() || numberOfTasks.Any())
            {
                int sum = taskTime.Peek() * numberOfTasks.Peek();

                if (sum <= 240)
                {
                    if (sum <= 60)
                    {
                        data["Darth Vader Ducky"]++;
                    }
                    else if (sum <= 120)
                    {
                        data["Thor Ducky"]++;
                    }
                    else if (sum <= 180)
                    {
                        data["Big Blue Rubber Ducky"]++;
                    }
                    else
                    {
                        data["Small Yellow Rubber Ducky"]++;
                    }

                    taskTime.Dequeue();
                    numberOfTasks.Pop();
                }
                else
                {
                    taskTime.Enqueue(taskTime.Dequeue());
                    numberOfTasks.Push(numberOfTasks.Pop() - 2);
                }

              
            }

            if (taskTime.Any() || numberOfTasks.Any()) return;
            Console.WriteLine("Congratulations, all tasks have been completed! Rubber ducks rewarded:");
            foreach (var duck in data)
            {
                Console.WriteLine($"{duck.Key}: {duck.Value}");
            }
        }
    }
}
