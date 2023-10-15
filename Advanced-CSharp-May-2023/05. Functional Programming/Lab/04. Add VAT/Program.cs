using System;
using System.Linq;

namespace _04._Add_VAT
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<double, string> VAT = x => (x * 1.20).ToString("F2"); 
            Console.WriteLine(string.Join(Environment.NewLine, 
                Console.ReadLine()
                    .Split(", ")
                    .Select(double.Parse)
                    .Select(VAT)));
        }
    }
}