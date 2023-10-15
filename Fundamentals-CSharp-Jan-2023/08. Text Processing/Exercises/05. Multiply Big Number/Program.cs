using System;
using System.Linq;
using System.Text;

namespace _05._Multiply_Big_Number
{
    class Program
    {
        static void Main(string[] args)
        {
            string n = Console.ReadLine();
            int multiply = int.Parse(Console.ReadLine());
            StringBuilder result = new StringBuilder();
            int reminder = 0;
            // Starts a for loop that iterates over the characters of the string "n" in reverse order
            for (int i = n.Length - 1; i >= 0; i--)
            {
                // // Multiplies the current digit of "n" by "multiply" and adds the "reminder" from the previous iteration
                int currDigit = int.Parse(n[i].ToString()) * multiply + reminder;
                // Appends the last digit of "currDigit" to the result
                result.Append(currDigit % 10); 
                // Update the "reminder" to the remaining digits of "currDigit"
                reminder = currDigit / 10;
            }

            // Checks if there is a "reminder" left after the loop has finished
            if (reminder != 0)
            {
                result.Append(reminder); // Appends the reminder in the result
            }

            // If multiply is 0 => Print 0
            // Else: => Concatenate the result and reverse it because the result is in reverse order and print it
            Console.WriteLine(multiply == 0 ? "0" : string.Join("", result.ToString().Reverse())); 
        }
    }
}