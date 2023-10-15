using System;
using System.Collections.Generic;
using System.Linq;

namespace DefiningClasses
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            List<Car> carsList = new List<Car>();
            int nOfCars = int.Parse(Console.ReadLine());
            for (int i = 0; i < nOfCars; i++)
            {
                string[] carInfo = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string model = carInfo[0];
                double fuelAmount = double.Parse(carInfo[1]);
                double fuelConsumption = double.Parse(carInfo[2]);

                Car car = new Car(model, fuelAmount, fuelConsumption);
                if (!carsList.Any())
                {
                    carsList.Add(car);
                }
                else if (carsList.Any(x => x.Model != model))
                {
                    carsList.Add(car);
                }
            }

            string input;
            while ((input = Console.ReadLine()) != "End")
            {
                string[] tokens = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string modelToDrive = tokens[1];
                double distance = double.Parse(tokens[2]);
                Car car = carsList.FirstOrDefault(x => x.Model == modelToDrive);
                if (car != null)
                {
                    car.Drive(distance);
                }
            }

            foreach (var car in carsList)
            {
                Console.WriteLine(car);
            }
        }
    }
}