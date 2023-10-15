using System;

namespace Spice_Must_Flow
{
    class Program
    {
        static void Main(string[] args)
        {
            uint yield = uint.Parse(Console.ReadLine()); // 111
            uint days = 0; // Days count
            uint spicesExtracted = 0; // Total Spices Extracted

            while (yield >= 100) // While our source is profitable
            {
                uint spices = yield; // Our spices is equal to our current working yield
                spices -= 26; // Workers consume 26 spices every shift
                spicesExtracted += spices; // Add the left spices to the total spices extracted 
                yield -= 10; // Yield drops by 10
                days++; // Count days
            }
            Console.WriteLine(days); // Print days

            // Workers can't consume more spice than there is in storage
            if (spicesExtracted >= 26) // If we have enough spices 
            {
                Console.WriteLine(spicesExtracted - 26); // Workers consume 26 of it
            }
            else // If we dont have enough spices => Workers don't consume
            {
                Console.WriteLine(spicesExtracted);
            }
        }
    }
}
