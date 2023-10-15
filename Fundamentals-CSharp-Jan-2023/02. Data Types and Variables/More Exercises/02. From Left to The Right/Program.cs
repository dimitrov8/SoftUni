namespace From_Left_to_RIght
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            int rows = int.Parse(Console.ReadLine()); // Input Rows

            for (int i = 0; i < rows; i++) // Print Rows
            {
                string[] numbers = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries); // Read numbers as a string

                long leftN = long.Parse(numbers[0]); // On index[0] is the leftN
                long rightN = long.Parse(numbers[1]); // On index[1] is the rightN
                int sum = 0; //  Sum
                
                if (leftN > rightN) // If leftN > rightN
                {
                    leftN = Math.Abs(leftN);
                    for (int l = 0; l <= leftN.ToString().Length - 1; l++)  // Loop to go by every digit of the biggest N
                    {
                        int currN = leftN.ToString()[l] - '0'; // Get the int value 
                        sum += currN; // Add it to the sum
                    }
                }
                else // If rightN > leftN
                {
                    rightN = Math.Abs(rightN);
                    for (int r = 0; r <= rightN.ToString().Length - 1; r++) // Loop to go by every digit of the biggest N
                    {
                        int currN = rightN.ToString()[r] - '0'; // Get the int value
                        sum += currN; // Add it to the sum
                    }
                }
                Console.WriteLine(sum); // Print the sum
            }
        }
    }
}
