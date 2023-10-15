using System;
using System.Collections.Generic;
using System.Linq;

namespace Special_Cars
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var tiresList = new List<Tires[]>();
            string command;
            while ((command = Console.ReadLine()) != "No more tires")
            {
                string[] tiresInfo = command.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                var currentTires = new List<Tires>();
                for (int i = 0; i < tiresInfo.Length; i += 2)
                {
                    int year = int.Parse(tiresInfo[i]);
                    double pressure = double.Parse(tiresInfo[i + 1]);
                    currentTires.Add(new Tires(year, pressure));
                }

                tiresList.Add(currentTires.ToArray());
            }

            var enginesList = new List<Engine>();
            while ((command = Console.ReadLine()) != "Engines done")
            {
                string[] engineInfo = command.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                int horsePower = int.Parse(engineInfo[0]);
                double cubicCapacity = double.Parse(engineInfo[1]);
                enginesList.Add(new Engine(horsePower, cubicCapacity));
            }

            var specialCars = new List<Car>();
            while ((command = Console.ReadLine()) != "Show special")
            {
                string[] carInfo = command.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string model = carInfo[0];
                string make = carInfo[1];
                int year = int.Parse(carInfo[2]);
                double fuelQuantity = double.Parse(carInfo[3]);
                double fuelConsumption = double.Parse(carInfo[4]);
                int engineIndex = int.Parse(carInfo[5]);
                int tiresIndex = int.Parse(carInfo[6]);

                Car currentCar = new Car(model, make, year, fuelQuantity, fuelConsumption, enginesList[engineIndex], tiresList[tiresIndex]);
                bool isSpecial = currentCar.isSpecial(currentCar);
                if (isSpecial)
                {
                    specialCars.Add(currentCar);
                }
            }

            if (specialCars.Any())
            {
                foreach (var car in specialCars)
                {
                    Console.WriteLine(car);
                }
            }
        }
    }
}