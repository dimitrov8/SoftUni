namespace Orders
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            int orders = int.Parse(Console.ReadLine());

            double coffeePrice = 0;
            double totalPrice = 0;
            for (int i = 1; i <= orders; i++)
            {
                double pricePerCapsule = double.Parse(Console.ReadLine());
                int day = int.Parse(Console.ReadLine());
                int capsuleCount = int.Parse(Console.ReadLine());

                coffeePrice = (day * capsuleCount) * pricePerCapsule;
                totalPrice += coffeePrice;

                Console.WriteLine($"The price for the coffee is: ${coffeePrice:f2}");
            }
            Console.WriteLine($"Total: ${totalPrice:f2}");
        }
    }
}
