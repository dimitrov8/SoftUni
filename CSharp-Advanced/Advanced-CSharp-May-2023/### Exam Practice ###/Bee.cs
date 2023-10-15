using System;

namespace Bee
{
    class Program
    {
        static void Main(string[] args)
        {
            int length = int.Parse(Console.ReadLine()!); // Read the length of the matrix (2D array)
            char[,] matrix = new char[length, length]; // Create the matrix
            AssignMatrixValues(matrix); // Assign the values to the matrix

            string command; // Initialize the command
            int pollinatedCount = 0; // Keep track of the number of pollinated flowers
            bool isLost = false; // Boolean used to keep track if the Bee is lost (got outside of the matrix bounds)
            Position beePosition = FindBeePosition(matrix, ref isLost); // Get the position of the Bee
            while ((command = Console.ReadLine()) != "End") // While the command is not "End"
            {
                ExecuteCommand(command, matrix, ref beePosition, ref pollinatedCount, ref isLost); // Go in the method: =>

                if (isLost) // If the Bee is lost: =>
                {
                    Console.WriteLine("The bee got lost!"); // Print
                    break; // Break
                }
            }

            // Print if the Bee managed to pollinate at least 5 flowers
            Console.WriteLine(pollinatedCount >= 5
                    ? $"Great job, the bee managed to pollinate {pollinatedCount} flowers!"
                    : $"The bee couldn't pollinate the flowers, she needed {5 - pollinatedCount} flowers more");

            // Print The 2D Array
            PrintMatrix(matrix);
        }
        
        private static void AssignMatrixValues(char[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++) // For each row in the matrix
            {
                string input = Console.ReadLine()!; // Read user input to store characters in the matrix
                for (int col = 0; col < matrix.GetLength(1); col++) // For each column in the matrix
                {
                    matrix[row, col] = input[col]; // Store the current character in matrix
                }
            }
        }

        private static Position FindBeePosition(char[,] matrix, ref bool isLost)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    if (matrix[row, col] == 'B') // If a Bee is found
                    {
                        return new Position(row, col); // Return the position of the Bee
                    }
                }
            }
            // Else a Bee is not found
            isLost = true; // Set the isLost flag to true
            return null; // Return null
        }
        
        private static void ExecuteCommand(string command, char[,] matrix, ref Position beePosition, ref int pollinatedCount, ref bool isLost)
        {
            int currentRow = beePosition.Row; // Variable to hold the current row and later check if the move is within the bound of the matrix
            int currentCol = beePosition.Col; // Variable to hold the current column and later check if a move is withing the bound of the matrix 

            switch (command) // User input command
            {
                // Every move moves the newly created variable and later we check if the move is within the bound of the matrix
                case "up":
                    currentRow--;
                    break;
                case "down":
                    currentRow++;
                    break;
                case "left":
                    currentCol--;
                    break;
                case "right":
                    currentCol++;
                    break;
            }

            // If the bound is not withing the matrix
            if (currentRow < 0 || currentRow >= matrix.GetLength(0) || currentCol < 0 || currentCol >= matrix.GetLength(1))
            {
                matrix[beePosition.Row, beePosition.Col] = '.'; // Remove the Bee from the matrix
                isLost = true; // Set the isLost flag to true
            }
            else // Else if the move is within the bounds of the matrix
            {
                char element = matrix[currentRow, currentCol]; // Get the element of the new position we want to move the Bee too

                matrix[currentRow, currentCol] = 'B'; // Move the Bee to the new position
                matrix[beePosition.Row, beePosition.Col] = '.'; // Remove the Bee from the old position

                // Updates the Bee position
                beePosition.Row = currentRow;
                beePosition.Col = currentCol;

                if (element == 'f') // If the element is 'f' then: =>
                {
                    pollinatedCount++; // Increment the number of pollinated flowers
                }
                else if (element == 'O') // If the element is 'O' then: =>
                {
                    ExecuteCommand(command, matrix, ref beePosition, ref pollinatedCount, ref isLost); // Go to the method again: =>
                }
            }
        }

        private static void PrintMatrix(char[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++) // For each row in the matrix
            {
                for (int col = 0; col < matrix.GetLength(1); col++) // For each column in the matrix
                {
                    Console.Write(matrix[row, col]); // Print the current element
                }

                Console.WriteLine();
            }
        }

        private class Position // Stores the Bee Position
        {
            public int Row { get; set; }
            public int Col { get; set; }

            public Position(int row, int col)
            {
                this.Row = row;
                this.Col = col;
            }
        }
    }
}
