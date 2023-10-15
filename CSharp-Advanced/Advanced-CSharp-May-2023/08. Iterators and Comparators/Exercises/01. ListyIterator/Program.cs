using System;
using System.Linq;

namespace ListyIterator
{
    class Program
    {
        static void Main(string[] args)
        {
            string command;
            ListyIterator<string> listyIterator = null;
            while ((command = Console.ReadLine()) != "END")
            {
                string[] tokens = command.Split(" ",StringSplitOptions.RemoveEmptyEntries);
                if (tokens[0] == "Create")
                {
                    listyIterator = new ListyIterator<string>(tokens.Skip(1).ToArray());
                }
                else if (tokens[0] == "Move")
                {
                    Console.WriteLine(listyIterator!.Move());
                }
                else if (tokens[0] == "HasNext")
                {
                    Console.WriteLine(listyIterator!.HasNext());
                }
                else if (tokens [0] == "Print")
                {
                    listyIterator!.Print();
                }
            }
        }
    }
}