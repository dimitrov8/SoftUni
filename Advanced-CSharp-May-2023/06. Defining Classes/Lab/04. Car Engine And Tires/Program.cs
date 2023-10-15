using System;

namespace CarManufacturer
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var tires = new Tire[4]
            {
                new Tire(1, 2.5),
                new Tire(1, 2.1),
                new Tire(2, 0.5),
                new Tire(2, 2.3)
            };
            var engine = new Engine(710 , 3994);
            var car = new Car("McLaren", "700S", 2023, 250, 9, engine, tires);
            Console.WriteLine(car);
        }
    }
}