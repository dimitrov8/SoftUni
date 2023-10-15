using System;
using System.ComponentModel;
using System.Formats.Asn1;
using System.Linq;

namespace _01._Smallest_of_Three_Numbers
{
    class Program
    {
        static void Main(string[] args)
        {
            int n1 = int.Parse(Console.ReadLine());
            int n2 = int.Parse(Console.ReadLine());
            int n3 = int.Parse(Console.ReadLine());

            int result = SmallestNumber(n1, n2, n3);
            Console.WriteLine(result);
        }

        static int SmallestNumber(int a, int b, int c)
        {
            int result = 0;
            if (a <= b && a <= c)
            {
                result = a;
            }
            else if (b <= a && b <= c)
            {
                result = b;
            }
            else if (c <= a && c <= b)
            {
                result = c;
            }

            return result;
        }
    }
}