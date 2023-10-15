using System;
using System.Collections.Generic;

namespace Sets_and_Dictionaries_Advanced_Exercise
{
    class Program
    {
        static void Main(string[] args)
        {
            int nOfInputs = int.Parse(Console.ReadLine());
            HashSet<string> usernames = new HashSet<string>();
            for (int i = 0; i < nOfInputs; i++)
            {
                string inputUsername = Console.ReadLine();
                usernames.Add(inputUsername);
            }

            foreach (var uniqueUsername in usernames)
            {
                Console.WriteLine(uniqueUsername);
            }
        }
    }
}