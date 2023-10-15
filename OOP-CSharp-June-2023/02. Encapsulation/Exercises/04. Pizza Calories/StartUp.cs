using System;

namespace PizzaCalories
{
    class StartUp
    {
        static void Main(string[] args)
        {
            RunEngine();
        }

        private static void RunEngine()
        {
            try
            {
                Pizza pizza = new Pizza(Console.ReadLine()?.Substring(6));
                string[] doughTokens = Console.ReadLine()!.Split();
                string flourType = doughTokens[1];
                string bakingTechnique = doughTokens[2];
                double weight = double.Parse(doughTokens[3]);
                pizza.Dough = new Dough(flourType, bakingTechnique, weight);

                string command;
                while ((command = Console.ReadLine()) != "END")
                {
                    string[] input = command!.Split();
                    string toppingType = input[1];
                    double toppingWeight = double.Parse(input[2]);
                    Topping topping = new Topping(toppingType, toppingWeight);

                    if (!pizza.ExceededNumberOfToppings())
                        pizza.AddTopping(topping);
                }

                Console.WriteLine(pizza.ToString());
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
            }
        }
    }
}