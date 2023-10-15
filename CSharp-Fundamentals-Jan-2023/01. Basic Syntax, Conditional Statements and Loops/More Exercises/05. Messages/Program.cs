namespace Messages
{
    using System;
    class Program
    {
        static void Main(string[] args)
        {
            int rows = int.Parse(Console.ReadLine()); // How many inputs are there

            string[] arr = new string[rows]; // Create array to store the result
            for (int i = 0; i < rows; i++)
            {
                int input = int.Parse(Console.ReadLine()); // Input

                if (input == 0)
                {
                    arr[i] += " "; // If input is 0. It's Space so we add that space to the arr
                    continue; // And read for next input
                }

                int inputLength = input.ToString().Length; // Length of Input

                int mainDigit = input % 10; // Find mainDigit

                int offset = (mainDigit - 2) * 3; // Find offset 

                if (mainDigit == 8 || mainDigit == 9) // If mainDigit == 8 || mainDigit == 9
                {
                    offset += 1; // Add 1 to the offset
                }

                int letterIndex = (offset + inputLength - 1) + 97; // Find letterIndex
                char letter = (char)letterIndex; // Letter
                arr[i] += letter;// Add the letter to the array
            }
            // Print the array
            Console.WriteLine(string.Join("", arr));
        }
    }
}
