using System;

namespace _04._Password_Validator
{
    class Program
    {
        static void Main(string[] args)
        {
            string password = Console.ReadLine();
            bool isLengthValid = IsPasswordLengthValid(password);
            bool isPasswordAlphaNumeric = IsPasswordAlphaNumeric(password);
            bool isPasswordAtLeastTwoDigits = IsPasswordAtLeastTwoDigits(password);

            if (!isLengthValid)
            {
                Console.WriteLine("Password must be between 6 and 10 characters");
            }

            if (!isPasswordAlphaNumeric)
            {
                Console.WriteLine("Password must consist only of letters and digits");
            }

            if (!isPasswordAtLeastTwoDigits)
            {
                Console.WriteLine("Password must have at least 2 digits");
            }

            else if (isLengthValid && isPasswordAlphaNumeric && isPasswordAtLeastTwoDigits)
            {
                Console.WriteLine("Password is valid");
            }
        }

        // Method to check if password is between 6 and 10 characters
        static bool IsPasswordLengthValid(string password)
        {
            // It returns true only if password length is between 6 and 10 characters
            bool isValid = password.Length >= 6 && password.Length <= 10;
            // Return
            return isValid;
        }

        // Method to check if password is alpha numeric(a-z && 0-9)
        static bool IsPasswordAlphaNumeric(string password)
        {
            // For each to check every character of the password
            foreach (char ch in password)
            {
                // If any of the characters is not alpha numeric(a-z && 0-9)
                if (!char.IsLetterOrDigit(ch))
                {
                    // Return false
                    return false;
                }
            }

            // Else return true 
            return true;
        }

        // Method to check if password has at least two digits
        static bool IsPasswordAtLeastTwoDigits(string password)
        {
            // Counter to count the digits
            int digitsCount = 0;
            // Foreach to check every character
            foreach (char ch in password)
            {
                // If any of the characters is a digit 
                if (char.IsDigit(ch))
                {
                    // Add 1 to the counter
                    digitsCount++;
                }
            }

            // If password has at least 2 digits => return true
            // Else it returns false
            return digitsCount >= 2;
        }
    }
}