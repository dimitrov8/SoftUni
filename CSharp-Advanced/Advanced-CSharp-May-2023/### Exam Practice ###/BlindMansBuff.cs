using System;
using System.Linq;

namespace BlindMansBuff
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] rowsAndCols = Console.ReadLine()!
                .Split()
                .Select(int.Parse)
                .ToArray();
            char[,] matrix = new char[rowsAndCols[0], rowsAndCols[1]];
            int[] position = new int[2];
            InitializeMatrix(matrix, position);

            int touchedOpponents = 0;
            int movesMade = 0;
            string command;
            while ((command = Console.ReadLine()) != "Finish")
            {
                int rowToMoveTo = position[0];
                int colToMoveTo = position[1];
                switch (command)
                {
                    case "up":
                        rowToMoveTo--;
                        break;
                    case "down":
                        rowToMoveTo++;
                        break;
                    case "left":
                        colToMoveTo--;
                        break;
                    case "right":
                        colToMoveTo++;
                        break;
                }

                if (rowToMoveTo < 0 || rowToMoveTo >= matrix.GetLength(0) || colToMoveTo < 0 || colToMoveTo >= matrix.GetLength(1))
                {
                    continue;
                }
                
                if (matrix[rowToMoveTo, colToMoveTo] == 'O')
                {
                    continue;
                }
                char element = matrix[rowToMoveTo, colToMoveTo];
                movesMade++;
                
                if (element == 'P')
                {
                    touchedOpponents++;
                    matrix[rowToMoveTo, colToMoveTo] = '-';
                    matrix[position[0], position[1]] = 'B';
                }

                position = new int[] { rowToMoveTo, colToMoveTo };

                if (touchedOpponents == 3)
                {
                    break;
                }
            }

            Console.WriteLine("Game over!");
            Console.WriteLine($"Touched opponents: {touchedOpponents} Moves made: {movesMade}");
        }

        static void InitializeMatrix(char[,] matrix, int[] position)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                char[] userInput = Console.ReadLine()!
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(char.Parse)
                    .ToArray();
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    char currentElement = userInput[col];
                    if (currentElement == 'B')
                    {
                        position[0] = row;
                        position[1] = col;
                    }

                    matrix[row, col] = currentElement;
                }
            }
        }
    }
}
