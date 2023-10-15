namespace Padawan_Equipment
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            double money = double.Parse(Console.ReadLine());
            int students = int.Parse(Console.ReadLine());
            double lightsaberPrice = double.Parse(Console.ReadLine());
            double robePrice = double.Parse(Console.ReadLine());
            double beltPrice = double.Parse(Console.ReadLine());

            double lsTotalPrice = lightsaberPrice * Math.Ceiling(students * 1.10);
            double robesTotalPrice = robePrice * students;
            double beltsTotalPrice = beltPrice * (students - students /  6);
            double totalPrice = lsTotalPrice + robesTotalPrice + beltsTotalPrice;

            if (money >= totalPrice)
            {
                Console.WriteLine($"The money is enough - it would cost {totalPrice:f2}lv.");
            }
            else
            {
                Console.WriteLine($"John will need {totalPrice - money:f2}lv more.");
            }
        }
    }
}
