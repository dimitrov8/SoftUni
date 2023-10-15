using System;
using System.Linq;

namespace Stack
{
    class Program
    {
        static void Main(string[] args)
        {
            string command;
            CustomStack<int> customStack = new CustomStack<int>();
            while ((command = Console.ReadLine()) != "END")
            {
                string[] tokens = command.Split(new[] {" ", ","}, StringSplitOptions.RemoveEmptyEntries);
                if (tokens[0] == "Push")
                {
                    customStack.Push(tokens.Skip(1).Select(int.Parse).ToArray());
                }
                else if (tokens[0] == "Pop")
                {
                    try
                    {
                        customStack.Pop();
                    }
                    catch (InvalidOperationException exception)
                    {
                        Console.WriteLine(exception.Message);
                    }
                }
            }

            foreach (var element in customStack)
            {
                Console.WriteLine(element);
            }

            foreach (var element in customStack)
            {
                Console.WriteLine(element);
            }
        }
    }
}