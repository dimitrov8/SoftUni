using System;

namespace Triples_Of_Latin_Letters
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine()); // N to which letter

            // We start from 'a'(it has value of 97) 
            // For loop while 'a'(97 +3) > firstLetter ===> firstLetter++
            for (char firstLetter = 'a'; firstLetter < 'a' + n; firstLetter++) 
            {
                // secondLetter we start again from 'a' and loop while 'a'(97 + 3) > secondLetter ==> secondLetter++
                for (char secondLetter = 'a'; secondLetter < 'a' + n; secondLetter++)
                {
                    // thirdLetter again we start from 'a' and loop 
                    for (char thirdLetter = 'a'; thirdLetter < 'a' + n; thirdLetter++)
                    {
                        // Print every triplets letters
                        Console.WriteLine($"{firstLetter}{secondLetter}{thirdLetter}");
                    }
                }
            }

        }
    }
}

