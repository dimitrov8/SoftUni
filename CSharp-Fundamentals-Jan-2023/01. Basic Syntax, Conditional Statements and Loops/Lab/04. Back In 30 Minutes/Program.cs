namespace Back_In_30_Minutes
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            int h = int.Parse(Console.ReadLine());
            int m = int.Parse(Console.ReadLine()) + 30;

            if (h >= 23)
            {
                h = 0;
                m -= 60;
            }
            if (m > 59)
            {
                h++;
                m -= 60;
            }
            Console.WriteLine($"{h:d1}:{m:d2}");
        }
    }
}
