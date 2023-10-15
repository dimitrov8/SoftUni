namespace SetCover
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    class StartUp
    {
        static void Main(string[] args)
        {
            // Read the universe from input,
            int[] universe = Console.ReadLine()!
                .Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            int numberOfSets = int.Parse(Console.ReadLine()!);  // Read the number of sets from input
            int[][] sets = new int[numberOfSets][]; // Create a jagged array to store the sets
            
            for (int row = 0; row < sets.Length; row++) // Loop through each row
            {
                // Values to be stored in the current row
                int[] rowsValue = Console.ReadLine()!
                    .Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();
                sets[row] = new int[rowsValue.Length]; // Set the length of columns in the current row

                for (int col = 0; col < sets[row].Length; col++) // Loop through each column
                {
                    sets[row][col] = rowsValue[col]; // Assign the current value to the current [row][column]
                }
            }

            List<int[]> selectedSets = ChooseSets(sets.ToList(), universe.ToList()); // Call the ChooseSets method to select sets that cover the universe
            Console.WriteLine($"Sets to take ({selectedSets.Count}):"); // Print the number of selected sets
            foreach (var set in selectedSets) // ForEach set in the selectedSets
            {
                Console.WriteLine($"{{ {string.Join(", ", set)} }}"); // Print each selected set
            }
        }

        public static List<int[]> ChooseSets(IList<int[]> sets, IList<int> universe)
        {
            var selectedSets = new List<int[]>(); // Create a list to store the selected sets
            while (universe.Count > 0) // Loop until the universe is covered
            {
                int[] longestSet = sets.MaxBy(s => s.Count(universe.Contains)); // Find the longest set that covers the most elements of the universe
                selectedSets.Add(longestSet); // Add the longest set to the selectedSets list
                sets.Remove(longestSet); // Remove the longest set from the sets list
                foreach (var item in longestSet) // ForEach item in the longestSet
                {
                    if (universe.Contains(item)) // If the universe contains the item
                    {
                        universe.Remove(item); // Remove the item
                    }
                }
            }
            
            return selectedSets; // Return the selectedSets list
        }
    }
}
