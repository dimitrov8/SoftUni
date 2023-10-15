using System;

namespace NavyBattle
{
    class Program
    {
        static void Main(string[] args)
        {
            int length = int.Parse(Console.ReadLine()!);
            char[,] matrix = new char[length, length];

            int[] submarinePosition = new int[2];
            InitializeMatrix(matrix, submarinePosition);

            int minesEncountered = 0;
            int battleCruisersToDestroy = 3;
            while (true)
            {
                int rowToMoveTo = submarinePosition[0];
                int colToMoveTo = submarinePosition[1];
                string command = Console.ReadLine();
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

                char element = matrix[rowToMoveTo, colToMoveTo];
                if (element == '*')
                {
                    minesEncountered++;
                }
                else if (element == 'C')
                {
                    battleCruisersToDestroy--;
                }

                matrix[submarinePosition[0], submarinePosition[1]] = '-';
                matrix[rowToMoveTo, colToMoveTo] = 'S';

                submarinePosition = new[] { rowToMoveTo, colToMoveTo };

                if (minesEncountered == 3 || battleCruisersToDestroy == 0)
                {
                    break;
                }
            }

            if (minesEncountered == 3)
            {
                Console.WriteLine($"Mission failed, U-9 disappeared! Last known coordinates [{submarinePosition[0]}, {submarinePosition[1]}]!");
            }
            else if (battleCruisersToDestroy == 0)
            {
                Console.WriteLine("Mission accomplished, U-9 has destroyed all battle cruisers of the enemy!");
            }
            
            PrintMatrix(matrix);
        }


        static void InitializeMatrix(char[,] matrix, int[] submarinePosition)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                char[] userInput = Console.ReadLine()!.ToCharArray();
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    char currentElement = userInput[col];

                    if (currentElement == 'S')
                    {
                        submarinePosition[0] = row;
                        submarinePosition[1] = col;
                    }

                    matrix[row, col] = currentElement;
                }
            }
        }

        static void PrintMatrix(char[,] matrix)
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
    }
}
