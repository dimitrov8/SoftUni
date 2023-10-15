using System;
using System.Collections.Generic;
using System.Linq;

namespace _06._Songs_Queue
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue<string> songs = new Queue<string>(Console.ReadLine()
                .Split(',', StringSplitOptions.TrimEntries));

            while (songs.Count > 0)
            {
                string[] tokens = Console.ReadLine().Split(' ');
                string mainCommand = tokens[0];
                if (mainCommand == "Play")
                {
                    songs.Dequeue();
                }
                else if (mainCommand == "Add")
                {
                    string songToAdd = string.Join(" ", tokens, 1, tokens.Length - 1);
                    if (songs.Any(s => s == songToAdd))
                    {
                        Console.WriteLine($"{songToAdd} is already contained!");
                    }
                    else
                    {
                        songs.Enqueue(songToAdd);
                    }
                }
                else if (mainCommand == "Show")
                {
                    Console.WriteLine(string.Join(", ", songs));
                }
            }

            Console.WriteLine("No more songs!");
        }
    }
}