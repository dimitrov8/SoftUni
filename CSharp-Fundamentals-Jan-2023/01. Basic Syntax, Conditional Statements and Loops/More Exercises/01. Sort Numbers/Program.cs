namespace Sort_Numbers
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            int n1 = int.Parse(Console.ReadLine());
            int n2 = int.Parse(Console.ReadLine());
            int n3 = int.Parse(Console.ReadLine());

            if (n3 > n2 && n3 > n1)
            {
                if (n2 > n1)
                {
                    Console.WriteLine($"{n3}\n{n2}\n{n1}");
                }
                else if (n1 > n2)
                {
                    Console.WriteLine($"{n3}\n{n1}\n{n2}");
                }
                else
                {
                    Console.WriteLine($"{n3}\n{n2}\n{n1}");
                }
            }
            else if (n2 > n3 && n2 > n1)
            {
                if (n3 > n1)
                {
                    Console.WriteLine($"{n2}\n{n3}\n{n1}");
                }
                else if (n1 > n3)
                {
                    Console.WriteLine($"{n2}\n{n1}\n{n3}");
                }
                else
                {
                    Console.WriteLine($"{n2}\n{n1}\n{n3}");
                }
            }
            else if (n1 > n2 && n1 > n3)
            {
                if (n2 > n3)
                {
                    Console.WriteLine($"{n1}\n{n2}\n{n3}");
                }
                else if (n3 > n2)
                {
                    Console.WriteLine($"{n1}\n{n3}\n{n2}");
                }
                else
                {
                    Console.WriteLine($"{n1}\n{n2}\n{n3}");
                }
            }
        }
    }
}
