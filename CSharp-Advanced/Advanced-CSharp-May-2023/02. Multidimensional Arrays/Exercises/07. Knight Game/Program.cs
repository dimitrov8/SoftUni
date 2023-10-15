using System;

namespace _7._Knight_Game
{
    class Program
    {
        static void Main(string[] args)
        {
            int boardSize = int.Parse(Console.ReadLine()); // Chess board size
            char[,] matrix = new Char[boardSize, boardSize]; // Assign the size of the matrix
            AssignValuesInTheMatrix(boardSize, matrix); // Assign values in the matrix

            if (boardSize < 3) // If the board size is too small for any knight moves
            {
                Console.WriteLine(0); // Print
                return; // Stop the program
            }

            // Logic
            int knightsRemoved = 0; // Keep track of knights removed

            while (true) // While true
            {
                int mostThreats = 0; // Keep track of the knight with the most threats
                int knightWithMostThreatsRow = -1; // Default value of the knight with the most threats row
                int knightWithMostThreatsCol = -1; // Default value of the knight with the most threats column
                for (int row = 0; row < boardSize; row++) // For each row
                {
                    for (int col = 0; col < boardSize; col++) // For each column
                    {
                        if (matrix[row, col] == 'K') // If the current row column contains a Knight
                        {
                            int currentThreats =
                                ThreatsCount(matrix, boardSize, row, col); // Calculate the current knight threats

                            if (currentThreats > mostThreats) // If the current knight threats are more than mostThreats
                            {
                                mostThreats = currentThreats; // Update the most threats variable
                                knightWithMostThreatsRow =
                                    row; // Store row position of the Knight with the most threats
                                knightWithMostThreatsCol =
                                    col; // Store the column position of the Knight with the most threats
                            }
                        }
                    }
                }

                if (mostThreats == 0) // If a knight doesn't make any threat
                {
                    break; // Break the loop
                }

                // If there is a knight with most threats
                matrix[knightWithMostThreatsRow, knightWithMostThreatsCol] = '0'; // Remove that knight
                knightsRemoved++; // Increase the knights removed count 
                // And continue searching for the next night with the most threats
            }

            Console.WriteLine(knightsRemoved); // Print the number of knights removed
        }

        private static int ThreatsCount(char[,] matrix, int boardSize, int row, int col)
        {
            int currentTakenKnights = 0;
            if (CellIsValid(row + 1, col + 2, boardSize) && matrix[row + 1, col + 2] == 'K')
            {
                currentTakenKnights++;
            }

            if (CellIsValid(row + 1, col - 2, boardSize) && matrix[row + 1, col - 2] == 'K')
            {
                currentTakenKnights++;
            }

            if (CellIsValid(row - 1, col + 2, boardSize) && matrix[row - 1, col + 2] == 'K')
            {
                currentTakenKnights++;
            }

            if (CellIsValid(row - 1, col - 2, boardSize) && matrix[row - 1, col - 2] == 'K')
            {
                currentTakenKnights++;
            }

            if (CellIsValid(row + 2, col + 1, boardSize) && matrix[row + 2, col + 1] == 'K')
            {
                currentTakenKnights++;
            }

            if (CellIsValid(row + 2, col - 1, boardSize) && matrix[row + 2, col - 1] == 'K')
            {
                currentTakenKnights++;
            }

            if (CellIsValid(row - 2, col + 1, boardSize) && matrix[row - 2, col + 1] == 'K')
            {
                currentTakenKnights++;
            }

            if (CellIsValid(row - 2, col - 1, boardSize) && matrix[row - 2, col - 1] == 'K')
            {
                currentTakenKnights++;
            }

            return currentTakenKnights;
        }

        private static void AssignValuesInTheMatrix(int boardSize, char[,] matrix)
        {
            for (int row = 0; row < boardSize; row++)
            {
                char[] values = Console.ReadLine()
                    .ToCharArray();
                for (int col = 0; col < boardSize; col++)
                {
                    matrix[row, col] = values[col];
                }
            }
        }

        private static bool CellIsValid(int row, int col, int boardSize) // Checks if a knight can make a valid move
        {
            return row >= 0 && row < boardSize && col >= 0 && col < boardSize;
        }
    }
}