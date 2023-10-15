using System;

namespace SuperMario
{
    class Program
    {
        static void Main(string[] args)
        {
            int marioHealth = int.Parse(Console.ReadLine()!); 

            int matrixSize = int.Parse(Console.ReadLine()!); 

            int[] marioPosition = new int[2]; 
            char[][] matrix = new char[matrixSize][]; 

            for (int row = 0; row < matrix.Length; row++) 
            {
                char[] currentRowElements = Console.ReadLine()!.ToCharArray(); 
                matrix[row] = currentRowElements; 
                for (int col = 0; col < matrix[row].Length; col++)
                {
                    if (matrix[row][col] == 'M')
                    {
                        marioPosition[0] = row;
                        marioPosition[1] = col;
                    }
                }
            }

            bool reachedPrincess = false;
            while (true)
            {
                string[] userCommandInfo = Console.ReadLine()!.Split();
                char directionToMoveTo = char.Parse(userCommandInfo[0]);
                int enemyRow = int.Parse(userCommandInfo[1]);
                int enemyColumn = int.Parse(userCommandInfo[2]);

                matrix[enemyRow][enemyColumn] = 'B';

                int rowToMoveTo = marioPosition[0];
                int colToMoveTo = marioPosition[1];
                switch (directionToMoveTo)
                {
                    case 'W':
                        rowToMoveTo--;
                        break;
                    case 'S':
                        rowToMoveTo++;
                        break;
                    case 'A':
                        colToMoveTo--;
                        break;
                    case 'D':
                        colToMoveTo++;
                        break;
                }

                marioHealth--;

                if (rowToMoveTo >= 0 && rowToMoveTo < matrix.Length && colToMoveTo >= 0 && colToMoveTo < matrix[rowToMoveTo].Length)
                {
                    char currentElement = matrix[rowToMoveTo][colToMoveTo];
                    matrix[marioPosition[0]][marioPosition[1]] = '-';
                    if (currentElement == 'B')
                    {
                        marioHealth -= 2;
                        matrix[rowToMoveTo][colToMoveTo] = 'M';
                    }
                    else if (currentElement == 'P')
                    {
                        reachedPrincess = true;
                        matrix[rowToMoveTo][colToMoveTo] = '-';
                        marioPosition = new int[] { rowToMoveTo, colToMoveTo };
                        break;
                    }

                    if (marioHealth <= 0)
                    {
                        matrix[rowToMoveTo][colToMoveTo] = 'X';
                        marioPosition = new int[] { rowToMoveTo, colToMoveTo };
                        break;
                    }

                    marioPosition = new int[] { rowToMoveTo, colToMoveTo };
                }
                else if (marioHealth <= 0)
                {
                    matrix[marioPosition[0]][marioPosition[1]] = 'X';
                    marioPosition[0] = rowToMoveTo;
                    marioPosition[1] = colToMoveTo;
                    break;
                }
            }


            Console.WriteLine(reachedPrincess
                ? $"Mario has successfully saved the princess! Lives left: {marioHealth}"
                : $"Mario died at {marioPosition[0]};{marioPosition[1]}.");

            PrintMatrix(matrix);
        }

        static void PrintMatrix(char[][] matrix)
        {
            for (int row = 0; row < matrix.Length; row++)
            {
                for (int col = 0; col < matrix[row].Length; col++)
                {
                    Console.Write(matrix[row][col]);
                }

                Console.WriteLine();
            }
        }
    }
}
