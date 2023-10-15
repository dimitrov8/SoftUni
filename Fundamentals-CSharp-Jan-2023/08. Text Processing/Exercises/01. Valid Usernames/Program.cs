using System;
using System.Linq;
using System.Text;

namespace _01._Valid_Usernames
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] usernames = Console.ReadLine().Split(", ");
            StringBuilder validUsernames = new StringBuilder();
            foreach (var username in usernames)
            {
                bool isValid = username.Length >= 3 && username.Length <= 16 &&
                               username.All(c => char.IsLetterOrDigit(c) || c == '-' || c == '_');
                if (isValid)
                {
                    validUsernames.AppendLine(username);
                }
            }

            Console.WriteLine(validUsernames);
        }
    }
}