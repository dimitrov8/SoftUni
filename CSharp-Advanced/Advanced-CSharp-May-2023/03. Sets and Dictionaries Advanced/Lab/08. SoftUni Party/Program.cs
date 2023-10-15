using System;
using System.Collections.Generic;
using System.Linq;

namespace _8._SoftUni_Party
{
    class Program
    {
        static void Main(string[] args)
        {
            HashSet<string> guests = new HashSet<string>();

            string input;
            while ((input = Console.ReadLine()) != "PARTY")
            {
                string guestNumber = input;
                guests.Add(guestNumber);
            }

            while ((input = Console.ReadLine()) != "END")
            {
                string guestNumber = input;
                guests.Remove(guestNumber);
            }

            Console.WriteLine(guests.Count);

            foreach (var guestN in guests.OrderByDescending(c => char.IsDigit(c[0])))
            {
                Console.WriteLine(guestN);
            }
        }
    }
}