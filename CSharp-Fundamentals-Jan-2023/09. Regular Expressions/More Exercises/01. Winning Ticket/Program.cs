using System;
using System.Security;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace WinningTicket
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] tickets = Console.ReadLine().Split(", ");
            Regex winningChars = new Regex(@"(\@{6,}|\#{6,}|\${6,}|\^{6,})");

            foreach (string ticket in tickets)
            {
                string trimmedTicket = ticket.Trim();
                if (trimmedTicket.Length != 20)
                {
                    Console.WriteLine("invalid ticket");
                    continue;
                }
                Match leftMatch = winningChars.Match(trimmedTicket.Substring(0, 10));
                Match rightMatch = winningChars.Match(trimmedTicket.Substring(10));

                if (leftMatch.Success && rightMatch.Success && leftMatch.Value[0] == rightMatch.Value[0])
                {
                    int matchLength = Math.Min(leftMatch.Length, rightMatch.Length);
                    if (matchLength == 10)
                    {
                        Console.WriteLine($"ticket \"{trimmedTicket}\" - 10{leftMatch.Value[0]} Jackpot!");
                    }
                    else
                    {
                        Console.WriteLine($"ticket \"{trimmedTicket}\" - {matchLength}{leftMatch.Value[0]}");
                    }
                }
                else
                {
                    Console.WriteLine($"ticket \"{trimmedTicket}\" - no match");
                }
            }
        }
    }
}