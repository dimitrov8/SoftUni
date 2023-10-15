namespace Theatre_Promotion
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            string dayType = Console.ReadLine();
            int age = int.Parse(Console.ReadLine());

            int price = 0;

            if (age >= 123|| age < 0)
            {
                Console.WriteLine("Error!");
                return;
            }

            switch (dayType)
            {
                case "Weekday":
                    if (age >= 0 && age < 19 || age >= 65 && age < 123)
                    {
                        price = 12;
                    }
                    else if (age >= 19 && age < 65)
                    {
                        price = 18;
                    }
                    break;
                case "Weekend":
                    if (age >= 0 && age < 19 || age >= 65 && age < 123)
                    {
                        price = 15;
                    }
                    else if (age >= 19 && age < 65)
                    {
                        price = 20;
                    }
                    break;
                case "Holiday":
                    if (age >= 0 && age < 19)
                    {
                        price = 5;
                    }
                    else if (age >= 19 && age < 65)
                    {
                        price = 12;
                    }
                    else
                    {
                        price = 10;
                    }
                    break;   
            }
            Console.WriteLine($"{price}$");
        }
    }
}
