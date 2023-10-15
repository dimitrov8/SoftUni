using System;

namespace Snake
{
    class Program
    {
        static void Main(string[] args)
        {
            int length = int.Parse(Console.ReadLine()!); // User input for the length of the matrix
            char[,] matrix = new char[length, length]; // Create the matrix with the specified length

            AssignMatrixValues(matrix); // Assign values to the matrix

            bool isOut = false; // Keeps track if the snake is out of the matrix
            int foodEaten = 0; // Keeps track of the food the snake ate
            Position snakePosition = FindSnake(matrix, ref isOut); // Get the position of the snake
            while (true)
            {
                if (isOut || foodEaten == 10) // If the snake is out or has eaten enough food
                {
                    break; // Break out of the loop
                }
                
                string command = Console.ReadLine(); // Read the command
                ExecuteCommand(snakePosition, command, matrix, ref isOut, ref foodEaten); // Method to execute the command: =>
            }

            // Output
            if (foodEaten < 10) // If the snake is out or has eaten enough food 
            {
                // Print
                Console.WriteLine("Game over!"); 
                Console.WriteLine($"Food eaten: {foodEaten}");
            }
            else // Else
            {
                // Print
                Console.WriteLine("You won! You fed the snake.");
                Console.WriteLine("Food eaten: 10");
            }
            
            PrintMatrix(matrix); // Print the matrix
        }

        private static void AssignMatrixValues(char[,] matrix) // Method to assigns the matrix values
        {
            for (int row = 0; row < matrix.GetLength(0); row++) // For each row
            {
                string input = Console.ReadLine()!; // Get the user input to assign 
                for (int col = 0; col < matrix.GetLength(1); col++) // For each column
                {
                    matrix[row, col] = input[col]; // Assign the current char to the current matrix[row, col]
                }
            }
        }
        
        private static Position FindSnake(char[,] matrix, ref bool isOut) // Method to finds the snake position
        {
            for (int row = 0; row < matrix.GetLength(0); row++) // For each row
            {
                for (int col = 0; col < matrix.GetLength(1); col++) // For each column
                {
                    if (matrix[row,col] == 'S') // If the snake is found
                    {
                        return new Position(row, col); // Return the snake position
                    }
                }
            }

            // ELse the snake does not exist
            isOut = true;  // Set the isOut flag to true
            return null;  // Return null
        }
        
        private static void ExecuteCommand(Position snakePosition, string command, char[,] matrix, ref bool isOut, ref int foodEaten) // Method to execute the command
        {
            // currentRow and currentColumn keep track of the user input moves
            int currentRow = snakePosition.Row;
            int currentColumn = snakePosition.Col;
            switch (command)
            {
                case "up":
                    currentRow--;
                    break;
                case "down":
                    currentRow++;
                    break;
                case "left":
                    currentColumn--;
                    break;
                case "right":
                    currentColumn++;
                    break;
            }

            // Checks if the desired position to move the snake to does not lead to it exiting the matrix: If it does =>
            if (currentRow < 0 || currentRow >= matrix.GetLength(0) || currentColumn < 0 || currentColumn >= matrix.GetLength(1))
            {
                matrix[snakePosition.Row, snakePosition.Col] = '.'; // Remove the snake from the matrix
                isOut = true; // Set the isOut flag to true
            }
            else // Else the desired position to move is within the matrix
            {
                char element = matrix[currentRow, currentColumn]; // Get the element of the position we want to move the snake to

                UpdateSnakePosition(matrix, currentRow, currentColumn, snakePosition); // Update the snake position

                if (element == '*') // If the element was '*': Its food
                {
                    foodEaten++; // Increment the food eaten
                }
                else if (element == 'B') // Else if the element was 'B': Its a burrow
                {
                    FindBurrow(matrix, snakePosition); // Goes into the "FindBurrow" method: Which finds the exit of the burrow and updates the snake position
                }
            }
        }

        private static void UpdateSnakePosition(char[,] matrix, int currentRow, int currentColumn, Position snakePosition) // Method to update the snake position
        {
            matrix[currentRow, currentColumn] = 'S'; // Move the snake to its new position
            matrix[snakePosition.Row, snakePosition.Col] = '.'; // Remove the snake from its old position

            // Update the snake position 
            snakePosition.Row = currentRow;
            snakePosition.Col = currentColumn;
        }

        private static void FindBurrow(char[,] matrix , Position snakePosition) // Method to find the exit of the burrow and updates the snake position
        {
            for (int row = snakePosition.Row; row < matrix.GetLength(0); row++) // For each row
            {
                for (int col = 0; col < matrix.GetLength(1); col++) // Fo reach column
                {
                    if (matrix[row,col] == 'B') // When we find the burrow
                    {
                        matrix[row, col] = 'S'; // Move the snake to the new position
                        matrix[snakePosition.Row, snakePosition.Col] = '.'; // Remove the snake from its old position
                        
                        // Update the snake position
                        snakePosition.Row = row;
                        snakePosition.Col = col;
                        return;
                    }
                }
            }
        }

        private static void PrintMatrix(char[,] matrix) // Method to print the matrix
        {
            for (int row = 0; row < matrix.GetLength(0); row++) // For each row
            {
                for (int col = 0; col < matrix.GetLength(1); col++) // For each column
                {
                    Console.Write(matrix[row,col]); // Print the current element in the matrix
                }

                Console.WriteLine();
            }
        }
    }

    public class Position // Keeps track of the snake position
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