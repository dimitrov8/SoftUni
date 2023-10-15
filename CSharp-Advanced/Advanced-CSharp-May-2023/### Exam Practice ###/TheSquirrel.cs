using System;

namespace TheSquirrel
{
    class Program
    {
        static void Main(string[] args)
        {
            int length = int.Parse(Console.ReadLine()!); // User input of the length of the matrix
            char[,] matrix = new char[length, length]; // Create the matrix

            int[] squirrelPosition = new int[2]; // Keeps track of the position of the squirrel
            string[] moves = Console.ReadLine()!.Split(", "); // User input moves 

            InitializeMatrix(matrix, squirrelPosition); // Initialize the matrix

            int collectedHazelnuts = 0; // Keep track of the collected hazelnuts

            bool isTrap = false; // Boolean indicating if the squirrel stepped on a trap
            bool isOut = false; // Boolean indicating if the squirrel is out of the matrix
            foreach (var move in moves) // For each move
            {
                // Create new variables for the row and column to the desired destination the user wants to move the squirrel to (starting from the squirrel position)
                int rowToMoveTo = squirrelPosition[0]; 
                int columnToMoveTo = squirrelPosition[1];

                switch (move) // For each move
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
                bool indicesAreValid = rowToMoveTo >= 0 && rowToMoveTo < matrix.GetLength(0) && columnToMoveTo >= 0 && columnToMoveTo < matrix.GetLength(1); // Boolean indicating if the new position the user wants to move the squirrel is within the bounds of the matrix

                matrix[squirrelPosition[0], squirrelPosition[1]] = '*'; // Remove the squirrel from its old position in all cases (even if the move is out of bounds)
                
                if (indicesAreValid) // If indices are valid: => Move the squirrel 
                {
                    char element = matrix[rowToMoveTo, columnToMoveTo]; // Get the element that was in the new position (hazelnut/trap/empty)
                    matrix[rowToMoveTo, columnToMoveTo] = 's'; // Move the squirrel to it's new position
                    if (element == 'h') // If the new position had hazelnut
                    {
                        collectedHazelnuts++; // Increment the collectedHazelnuts counter
                    }
                    else if (element == 't') // If there was a trap in the new position
                    {
                        isTrap = true;
                        Console.WriteLine("Unfortunately, the squirrel stepped on a trap..."); // Print
                        matrix[rowToMoveTo, columnToMoveTo] = '*'; // Remove the squirrel from the matrix
                        break; // Break out of the loop
                    }

                    squirrelPosition = new[] { rowToMoveTo, columnToMoveTo }; // In any case update the squirrel position

                    if (collectedHazelnuts == 3) // If the collectedHazelnuts become 3
                    {
                        Console.WriteLine("Good job! You have collected all hazelnuts!"); // Print
                        break; // Break out of the loop
                    }
                }
                else // If indices are not valid (it's out of the matrix)
                {
                    isOut = true;
                    Console.WriteLine("The squirrel is out of the field."); // Print
                    break; // Break out of the loop
                }
            }

            if (collectedHazelnuts != 3 && !isTrap && !isOut) // If the squirrel did not collect enough hazelnuts and did not step on a trap and is still in the matrix
            {
                Console.WriteLine("There are more hazelnuts to collect."); // Print
            }

            Console.WriteLine($"Hazelnuts collected: {collectedHazelnuts}"); // Print
        }

        static void InitializeMatrix(char[,] matrix, int[] squirrelPosition) // Initialize the matrix
        {
            for (int row = 0; row < matrix.GetLength(0); row++) // For each row
            {
                char[] userInput = Console.ReadLine()!.ToCharArray(); // Get the user input values

                for (int col = 0; col < matrix.GetLength(1); col++) // For each column
                {
                    char currentElement = userInput[col]; // Get the current element

                    if (currentElement == 's') // If the current element is the squirrel 
                    {
                        // Save it's position
                        squirrelPosition[0] = row;
                        squirrelPosition[1] = col;
                    }

                    matrix[row, col] = currentElement; // Assign the current element to the matrix
                }
            }
        }
    }
}
