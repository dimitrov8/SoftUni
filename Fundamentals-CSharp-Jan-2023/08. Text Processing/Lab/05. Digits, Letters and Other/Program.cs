using System;
using System.Text;

namespace _05._Digits_Letters_and_Other
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            StringBuilder digits = new StringBuilder();
            StringBuilder letters = new StringBuilder();
            StringBuilder symbols = new StringBuilder();
            foreach (var @char in input)
            {
                if (char.IsDigit(@char))
                {
                    digits.Append(@char);
                }
                else if (char.IsLetter(@char))
                {
                    letters.Append(@char);
                }
                else
                {
                    symbols.Append(@char);
                }
            }

            Console.WriteLine(string.Join(Environment.NewLine, digits, letters, symbols));
        }
    }
}