namespace SumOfCoins
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            // Read available coins from input
            int[] availableCoins = Console.ReadLine()!
                .Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            int targetSum = int.Parse(Console.ReadLine()!); // Read the target sum from input
            try
            {
                // Call the ChooseCoins method to calculate the selected coins
                Dictionary<int, int> selectedCoins = ChooseCoins(availableCoins, targetSum)
                    .Where(coin => coin.Value != 0) // Filter out coins with zero count
                    .OrderByDescending(coin => coin.Key) // Order coins by descending value
                    .ToDictionary(coin => coin.Key, coin => coin.Value); // Convert to a dictionary
                
                // Print the number of coins selected to reach the target sum
                Console.WriteLine($"Number of coins to take: {selectedCoins.Values.Sum()}");
                foreach (var kvp in selectedCoins) // Print each coin with its count
                { 
                    Console.WriteLine($"{kvp.Value} coin(s) with value {kvp.Key}"); 
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message); // Handle exception and print the error message
            }
        }

        public static Dictionary<int, int> ChooseCoins(IList<int> coins, int targetSum)
        {
            int[] sortedCoins = coins.OrderByDescending(coin => coin).ToArray(); // Sort coins in descending order
            var chosenCoins = new Dictionary<int, int>(); // Dictionary to store the chosen coins and their counts
            int currentSum = 0; // Initialize variables for current sum
            int coinIndex = 0; // Variable which keeps track of the coin index
            while (currentSum != targetSum && coinIndex < sortedCoins.Length) // Loop until the current sum equals the target sum or all coins are checked
            {
                int currentCoinValue = sortedCoins[coinIndex]; // Current coin value
                int reminder = targetSum - currentSum; // Calculate the remaining amount to reach the target sum
                int numberOfCoins = reminder / currentCoinValue; // Calculate the number of coins needed for the current coin value
 
                // Add or update the coin count in the chosenCoins dictionary
                if (chosenCoins.ContainsKey(currentCoinValue))
                {
                    chosenCoins[currentCoinValue] = numberOfCoins;
                }
                else
                {
                    chosenCoins.Add(currentCoinValue, numberOfCoins);
                }

                currentSum += currentCoinValue * numberOfCoins; // Update the current sum by adding the value of the chosen coins
                coinIndex++; // Move to the next coin index
            }

            if (currentSum != targetSum) // If the current sum is not equal to the target sum, throw an exception
            {
                throw new InvalidOperationException("Error");
            }

            return chosenCoins; // Return the chosenCoins dictionary
        }
    }
}