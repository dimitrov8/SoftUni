namespace KaminoFactory
{
    using System;
    using System.Linq;
    class Program
    {
        static void Main(string[] args)
        {
            int seqLength = int.Parse(Console.ReadLine()); // DNA Length
            int[] DNA = new int[seqLength]; // Starting DNA 

            int DNACount = -1;
            int dnaSample = 0;
            int DNAStartIndex = 0;
            int DNASum = 0;

            int sample = 0;
            string command;
            while ((command = Console.ReadLine()) != "Clone them!")
            {
                sample++;
                // ----------------CURRENT DNA INFO ----------------
                int[] currDNA = (command) // Recive new DNA
                    .Split("!", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                int countInARow = 0; // In a row counter
                int bestCountInARow = 0;
                int currEndIndex = 0;
                int currStartIndex = 0;
                int currDNASum = 0;
                bool isBetter = false;
                for (int i = 0; i < currDNA.Length; i++)
                {
                    // Task: Get the longest 1-s in a row seq
                    if (currDNA[i] != 1) // If current[i] is not 1
                    {
                        countInARow = 0; // Resent countInARow
                        continue;
                    }

                    countInARow++; // If it's 1 countInARow++
                    if (countInARow > bestCountInARow) // If our current count > bestCount
                    {
                        bestCountInARow = countInARow; // Store current count in bestCount
                        currEndIndex = i;
                    }
                }

                currStartIndex = currEndIndex - bestCountInARow + 1;
                currDNASum = currDNA.Sum();

                // ---------------- CHECK CURRENT DNA WITH BEST DNA ----------------

                if (bestCountInARow > DNACount)
                {
                    isBetter = true;
                }
                else if (bestCountInARow == DNACount)
                {
                    if (currStartIndex < DNAStartIndex)
                    {
                        isBetter = true;
                    }
                    else if (currStartIndex == DNAStartIndex)
                    {
                        if (currDNASum > DNASum)
                        {
                            isBetter = true;
                        }
                    }
                }

                if (isBetter)
                {
                    DNA = currDNA;
                    DNACount = bestCountInARow;
                    DNAStartIndex = currStartIndex;
                    DNASum = currDNASum;
                    dnaSample = sample;
                }
            }
            Console.WriteLine($"Best DNA sample {dnaSample} with sum: {DNASum}.");
            Console.WriteLine(string.Join(" ", DNA));
        }
    }
}