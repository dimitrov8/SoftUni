namespace Strong_Number
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            int input = int.Parse(Console.ReadLine()); // Original input

            string textInput = input.ToString(); // Convert the input to String

            int sum = 0; // Variable for sum

            for (int i = 0; i < textInput.Length; i++) // Loop for every single number in the console
            {
                int result = 0; // Reset the result for next number 
                double currDigitDouble = char.GetNumericValue(textInput[i]); // Get currentDigit as a number
                int currDigit = (int)(currDigitDouble); // Cast it to int

                if (currDigit != 0 && currDigit != 1) // If it's not 0 and it's not 1 ==>
                                                      // We add && currDigit != 1 ,because in our loop below we subtract(-) currDigit - 1;
                                                      // And if our currDigit is 1 our factoriel is gonna be 0. Which is not right
                {
                    for (int factoriel = currDigit - 1; factoriel >= 1; factoriel--) // Multiply from biggest to lowest factoriel
                    {
                        result = currDigit * factoriel; // Find the result by currentDigit * factoriel
                        currDigit = result; // Assign new value to currentDigit.
                                            // (We Assign the result).
                                            // So on the next loop it multiplies with the new currentDigit
                    }
                    sum += result;  // Add the current result to the sum
                }
                else // If currentDigit is 0
                {
                    sum += 1; // We add 1 to the sum, because 0! is 1
                }
            }
            if (input == sum) // Print
            {
                Console.WriteLine("yes");
            }
            else // Print
            {
                Console.WriteLine("no");
            }
        }
    }
}