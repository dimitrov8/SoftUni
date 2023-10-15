using System;
using System.Collections.Generic;
using System.Linq;

namespace DefiningClasses
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            int nOfCars = int.Parse(Console.ReadLine());
            List<Car> allCars = new List<Car>();
            for (int i = 0; i < nOfCars; i++)
            {
                string[] carInfo = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string model = carInfo[0]; // Car model
                // Tires
                double[] currentTiresPressure = { double.Parse(carInfo[5]), double.Parse(carInfo[7]), double.Parse(carInfo[9]), double.Parse(carInfo[11]) }; // Tires pressure
                int[] currentTiresAge = { int.Parse(carInfo[6]), int.Parse(carInfo[8]), int.Parse(carInfo[10]), int.Parse(carInfo[10]) }; // Tires Age
                Engine currentEngine = new Engine(int.Parse(carInfo[1]), int.Parse(carInfo[2])); // Engine (Speed and Power)
                Cargo currentCargo = new Cargo(carInfo[4], int.Parse(carInfo[3])); // Cargo (Type and Weight)
                Tires currentTires = new Tires(currentTiresPressure, currentTiresAge); // Tires (Pressure and Age)

                Car car = new Car(model, currentEngine, currentCargo, currentTires); // Create the car
                allCars.Add(car); // Add the car to the list
            }

            string typeOfCarsToPrint = Console.ReadLine(); // Receive a command
            if (typeOfCarsToPrint == "fragile") // If the command is "fragile"
            {
                foreach (var car in allCars.Where(x => x.Cargo.Type == "fragile" && x.Tires.Pressure.Any(x => x < 1)))
                {
                    Console.WriteLine(car);
                }
            }
            else if (typeOfCarsToPrint == "flammable") // Else if the command is "flammable"
            {
                foreach (var car in allCars.Where(x => x.Cargo.Type == "flammable" && x.Engine.Power > 250))
                {
                    Console.WriteLine(car);
                }
            }
        }
    }
}