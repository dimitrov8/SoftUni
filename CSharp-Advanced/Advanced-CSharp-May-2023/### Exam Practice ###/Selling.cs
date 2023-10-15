using System;

namespace Selling
{
    class Program
    {
        static void Main(string[] args)
        {
            int length = int.Parse(Console.ReadLine()!); // User input length of the matrix
            char[,] matrix = new char[length, length]; // Create the matrix

            Position playerPosition = new Position(-1, -1); // Used to store the player position
            Position lastPillarPosition = new Position(-1, -1); // User to store the last position of the pillar(there might be no pillars)
            AssignMatrixValues(matrix, ref playerPosition, ref lastPillarPosition);

            int totalMoney = 0; // Total money
            while (totalMoney < 50) // While we have not collected enough money
            {
                string command = Console.ReadLine(); // Read the command
                int rowToMoveTo = playerPosition.Row; // Initialize the row to move to
                int columnToMoveTo = playerPosition.Col; // Initialize the column to move

                switch (command) // Switch by the command
                {
                    case "up":
                        rowToMoveTo--;
                        break;
                    case "down":
                        rowToMoveTo++;
                        break;
                    case "left":
                        columnToMoveTo--;
                        break;
                    case "right":
                        columnToMoveTo++;
                        break;
                }

                // If the move leads the player outside the matrix
                if (rowToMoveTo < 0 || rowToMoveTo >= matrix.GetLength(0) || columnToMoveTo < 0 || columnToMoveTo >= matrix.GetLength(1))
                {
                    matrix[playerPosition.Row, playerPosition.Col] = '-'; // Remove the player from the matrix
                    Console.WriteLine("Bad news, you are out of the bakery."); // Print
                    break; // Break out of the loop
                }

                // Else if the move is within the matrix
                char element = matrix[rowToMoveTo, columnToMoveTo]; // Get the element at the position the player is going to move
                matrix[playerPosition.Row, playerPosition.Col] = '-'; // Remove the player from its old position
                if (char.IsDigit(element)) // If the element is a digit
                {
                    totalMoney += int.Parse(element.ToString()); // Add the digit to the totalMoney
                    UpdatePosition(matrix, rowToMoveTo, columnToMoveTo, playerPosition); // Update the position of the player
                }
                else if (element == 'O') // If the element is a pillar
                {
                    matrix[rowToMoveTo, columnToMoveTo] = '-'; // Remove the player from its old position
                    matrix[lastPillarPosition.Row, lastPillarPosition.Col] = 'S'; // Move the player to the last position of the pillar

                    // Update the position of the player
                    playerPosition.Row = lastPillarPosition.Row; 
                    playerPosition.Col = lastPillarPosition.Col;
                }
                else // Else if its an empty space
                {
                    matrix[rowToMoveTo, columnToMoveTo] = 'S'; // Move the player to the new position
                    
                    // Update the position of the player
                    playerPosition.Row = rowToMoveTo; 
                    playerPosition.Col = columnToMoveTo;
                    
                    if (char.IsDigit(element)) // If at the new position there was a digit
                    {
                        totalMoney += int.Parse(element.ToString()); // Add the digit to the totalMoney
                    }
                     
                    else if (element == 'O') // If at the new position there was a pillar
                    {
                        matrix[lastPillarPosition.Row, lastPillarPosition.Col] = 'S'; // Move the player to the last position of the pillar

                        // Update the position of the player
                        playerPosition.Row = lastPillarPosition.Row;
                        playerPosition.Col = lastPillarPosition.Col;
                    }
                }
            }

            if (totalMoney >= 50) // If the player collected enough money
            {
                Console.WriteLine("Good news! You succeeded in collecting enough money!"); // Print
            }

            // Print
            Console.WriteLine($"Money: {totalMoney}");
            PrintMatrix(matrix);
        }

        private static void UpdatePosition(char[,] matrix, int rowToMoveTo, int columnToMoveTo, Position playerPosition)
        {
            matrix[rowToMoveTo, columnToMoveTo] = 'S';

            playerPosition.Row = rowToMoveTo;
            playerPosition.Col = columnToMoveTo;
        }

        private static void AssignMatrixValues(char[,] matrix, ref Position position, ref Position lastPillarPosition)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                string input = Console.ReadLine()!;
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    char currentElement = input[col];
                    if (currentElement == 'S') // If the current element is the player
                    {
                        position = new Position(row, col); // Store the position of the player
                    }
                    else if (currentElement == 'O') // If the current element is a pillar
                    {
                        lastPillarPosition = new Position(row, col); // Store the position of the pillar
                    }

                    matrix[row, col] = currentElement; // Assign the current element to the matrix
                }
            }
        }

        private static void PrintMatrix(char[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    Console.Write(matrix[row, col]);
                }

                Console.WriteLine();
            }
        }

        private class Position
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