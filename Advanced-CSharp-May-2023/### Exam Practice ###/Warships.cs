using System;
using System.Linq;

namespace Warships
{
    class Program
    {
        static void Main(string[] args)
        {
            int matrixSize = int.Parse(Console.ReadLine()!); // User input for the matrix size
            char[,] matrix = new char[matrixSize, matrixSize]; // Create the matrix

            int[] attackPositions = Console.ReadLine()! // User input for the attack positions
                .Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            
            int playerOneShips = 0; // Total Count of the player one ships
            int playerTwoShips = 0; // Total Count of the player two ships
            
            // Assign values in the matrix
            AssignMatrixValues(matrix, ref playerOneShips, ref playerTwoShips);

            int sumAllShips = playerOneShips + playerTwoShips; // Total count of all ships in the matrix
            bool playerOneWon = false; // Boolean flag to keep track if player one won
            bool playerTwoWon = false; // Boolean flag to keep track if player two won

            // Logic for the attacking
            for (int i = 0; i < attackPositions.Length; i+= 2) 
            {
                // Current attack position
                int currentRow = attackPositions[i]; 
                int currentColumn = attackPositions[i + 1];

                // If the attack position is valid
                if (IsValidIndices(matrix, currentRow, currentColumn))
                {
                    // Get the current element at the attack position
                    char currentElement = matrix[currentRow, currentColumn];
                    if (currentElement == '>') // If the current element is one of the player two ships
                    {
                        playerTwoShips--; // Decrement the total number of ships that player two has
                        matrix[currentRow, currentColumn] = 'X'; // Remove the ship from the matrix

                        if (playerTwoShips == 0) // If player two has no ships
                        {
                            playerOneWon = true; // Player one wins
                            break; // Break out of the loop
                        }
                    }
                    else if (currentElement == '<') // If the current element is one of the player one ships 
                    {
                        playerOneShips--; // Decrement the number of ships that player one has
                        matrix[currentRow, currentColumn] = 'X'; // Remove the ship from the matrix

                        if (playerOneShips <= 0) // If player one has no ships
                        {
                            playerTwoWon = true; // Player two wins
                            break; // Break out of the loop
                        }
                    }
                    else if (currentElement == '#') // If the current element is a mine
                    {
                        ValidateMineCoordinates(matrix, currentRow, currentColumn); // Logic for the mine to destroy adjacent fields
                        int[] shipsCount = GetShips(matrix); // Get the count of ships in the matrix
                        playerOneShips = shipsCount[0]; // Player one ships count
                        playerTwoShips = shipsCount[1]; // Player two ships count

                        if (playerOneShips == 0) // If player one has no ships
                        {
                            playerTwoWon = true; // Player two wins
                            break; // Break out of the loop
                        }
                        
                        if (playerTwoShips == 0) // If player two has no ships 
                        {
                            playerOneWon = true; // Player one wins
                            break; // Break out of the loop
                        }
                    }
                }
            }
            
            // Total defeated ships
            int defeatedShips = sumAllShips - (playerOneShips + playerTwoShips);

            if (playerOneWon)
            {
               Console.WriteLine($"Player One has won the game! {defeatedShips} ships have been sunk in the battle.");
            }
            else if (playerTwoWon)
            {
                Console.WriteLine($"Player Two has won the game! {defeatedShips} ships have been sunk in the battle.");
            }
            else
            {
                Console.WriteLine($"It's a draw! Player One has {playerOneShips} ships left. Player Two has {playerTwoShips} ships left.");
            }
        }

        private static void AssignMatrixValues(char[,] matrix, ref int playerOneShips, ref int playerTwoShips) // Assigns the matrix values
        {
            for (int row = 0; row < matrix.GetLength(0); row++) // For each row
            {
                char[] input = Console.ReadLine()! // User input
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(char.Parse)
                    .ToArray();
                for (int col = 0; col < matrix.GetLength(1); col++) // For each column
                {
                    char currentElement = input[col]; // Get the current element
                    if (currentElement == '<') // If the current element is a ship owned by player one
                    {
                        playerOneShips++; // Increment the player one ships
                    }
                    else if (currentElement == '>') // If the current element is a ship owned by player two
                    {
                        playerTwoShips++; // Increment the player two ships
                    }

                    matrix[row, col] = currentElement; // Assign the current element
                }
            }
        }


        static bool IsValidIndices(char[,] matrix, int row, int col) // Returns true/false if the indices are valid
        {
            return row >= 0 && row < matrix.GetLength(0) && col >= 0 && col < matrix.GetLength(1);
        }

        static void ValidateMineCoordinates(char[,] matrix, int row, int col) // Destroys adjacent fields
        {
            int[] dx = { -1, 1, 0, 0, -1, 1, -1, 1 };
            int[] dy = { 0, 0, -1, 1, -1, 1, 1, -1 };

            for (int i = 0; i < dx.Length; i++) // For each possible move
            {
                int newRow = row + dx[i]; // Get the new row
                int newCol = col + dy[i]; // Get the new col

                if (IsValidIndices(matrix, newRow, newCol)) // If the indices are valid 
                {
                    matrix[newRow, newCol] = 'X'; // Destroy the current element in that matrix[newRow, newCol]
                }
            }
        }

        static int[] GetShips(char[,] matrix) // Returns the count of ships owned by player one and player two
        {
            int playerOneShips = matrix.Cast<char>().Count(c => c == '<');
            int playerTwoShips = matrix.Cast<char>().Count(c => c == '>');

            return new int[] { playerOneShips, playerTwoShips };
        }
    }
}