namespace Vacation
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            int peopleCount = int.Parse(Console.ReadLine());
            string groupType = Console.ReadLine();
            string day = Console.ReadLine();

            double price = 0;
            switch (day)
            {
                case "Friday":
                    if (groupType == "Students")
                    {
                        price = 8.45 * peopleCount;
                    }
                    else if (groupType == "Business")
                    {
                        if (peopleCount >= 100)
                        {
                            peopleCount -= 10;
                        }
                        price = 10.90 * peopleCount;
                    }
                    else if (groupType == "Regular")
                    {
                        price = 15 * peopleCount;
                    }
                    break;
                case "Saturday":
                    if (groupType == "Students")
                    {
                        price = 9.80 * peopleCount;
                    }
                    else if (groupType == "Business")
                    {
                        if (peopleCount >= 100)
                        {
                            peopleCount -= 10;
                        }
                        price = 15.60 * peopleCount;
                    }
                    else if (groupType == "Regular")
                    {
                        price = 20 * peopleCount;
                    }
                    break;
                case "Sunday":
                    if (groupType == "Students")
                    {
                        price = 10.46 * peopleCount;
                    }
                    else if (groupType == "Business")
                    {
                        if (peopleCount >= 100)
                        {
                            peopleCount -= 10;
                        }
                        price = 16 * peopleCount;
                    }
                    else if (groupType == "Regular")
                    {
                        price = 22.50 * peopleCount;
                    }
                    break;
            }

            if (groupType == "Students" && peopleCount >= 30)
            {
                price *= 0.85;
            }
            else if (groupType == "Regular" && peopleCount >= 10 && peopleCount <= 20)
            {
                price *= 0.95;
            }
            Console.WriteLine($"Total price: {price:f2}");
        }
    }
}
