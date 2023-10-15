using System;
using System.Collections.Generic;
using System.Linq;

namespace DefiningClasses
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            List<Engine> enginesList = new List<Engine>();
            List<Car> carsList = new List<Car>();

            int nOfEngines = int.Parse(Console.ReadLine());
            for (int i = 0; i < nOfEngines; i++)
            {
                string[] engineInfo = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string model = engineInfo[0];
                int power = int.Parse(engineInfo[1]);
                Engine engine = new Engine();

                if (engineInfo.Length == 3)
                {
                    if (char.IsDigit(engineInfo[2][0]))
                    {
                        int displacement = int.Parse(engineInfo[2]);
                        engine = new Engine(model, power, displacement);
                    }
                    else
                    {
                        string efficiency = engineInfo[2];
                        engine = new Engine(model, power, efficiency);
                    }
                }
                else if (engineInfo.Length == 4)
                {
                    int displacement = int.Parse(engineInfo[2]);
                    string efficiency = engineInfo[3];
                    engine = new Engine(model, power, displacement, efficiency);
                }
                else if (engineInfo.Length == 2)
                {
                    engine = new Engine(model, power);
                }
                enginesList.Add(engine);
            }

            int nOfCars = int.Parse(Console.ReadLine());
            for (int i = 0; i < nOfCars; i++)
            {
                string[] carInfo = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string model = carInfo[0];
                Engine engine = enginesList.First(x => x.Model == carInfo[1]);
                Car car = new Car();

                if (carInfo.Length == 3)
                {
                    if (char.IsDigit(carInfo[2][0]))
                    {
                        int weight = int.Parse(carInfo[2]);
                        car = new Car(model, engine, weight);
                    }
                    else
                    {
                        string color = carInfo[2];
                        car = new Car(model, engine, color);
                    }
                }
                else if (carInfo.Length == 4)
                {
                    int weight = int.Parse(carInfo[2]);
                    string color = carInfo[3];

                    car = new Car(model, engine, weight, color);
                }
                else if (carInfo.Length == 2)
                {
                    car = new Car(model, engine);
                }
                carsList.Add(car);
            }

            foreach (var car in carsList)
            {
                Console.WriteLine(car);
            }
        }
    }
}