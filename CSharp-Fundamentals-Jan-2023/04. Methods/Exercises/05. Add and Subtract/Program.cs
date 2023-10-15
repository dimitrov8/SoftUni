_Add_and_Subtractusing System;

namespace _05._Add_and_Subtract
{
    class Program
    {
        static void Main(string[] args)
        {
            int n1 = int.Parse(Console.ReadLine());
            int n2 = int.Parse(Console.ReadLine());
            int n3 = int.Parse(Console.ReadLine());

            int sum = GetAddSum(n1, n2);
            sum = GetSubtractSum(sum, n3);
            Console.WriteLine(sum);
        }

        static int GetAddSum(int a, int b)
        {
            return a + b;
        }
        
        static int GetSubtractSum(int sum, int c)
        {
            return sum - c;
        }
    }
}