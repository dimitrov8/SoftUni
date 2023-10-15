using System;
using System.Collections.Generic;
using System.Linq;

namespace _04._Raw_Data
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            List<Car> carList = new List<Car>();
            for (int i = 1; i <= n; i++)
            {
                string[] carInfo = Console.ReadLine().Split(" ");
                string carModel = carInfo[0];
                int engineSpeed = int.Parse(carInfo[1]);
                int enginePower = int.Parse(carInfo[2]);
                int cargoWeight = int.Parse(carInfo[3]);
                string cargoType = carInfo[4];
                Car car = new Car(carModel, engineSpeed, enginePower, cargoWeight, cargoType);

                carList.Add(car);
            }

            string carsToPrint = Console.ReadLine();

            Car.PrintDesiredTypeOfCars(carsToPrint, carList);
        }
    }

    public class Car
    {
        private string Model { get; set; }
        private int EngineSpeed { get; set; }
        private int EnginePower { get; set; }
        private int CargoWeight { get; set; }

        private string CargoType { get; set; }

        public Car(string carModel, int engineSpeed, int enginePower, int cargoWeight, string cargoType)
        {
            Model = carModel;
            EngineSpeed = engineSpeed;
            EnginePower = enginePower;
            CargoWeight = cargoWeight;
            CargoType = cargoType;
        }

        public static void PrintDesiredTypeOfCars(string carsToPrint, List<Car> carList)
        {
            if (carsToPrint == "fragile")
            {
                foreach (Car car in carList.Where(c => c.CargoType == "fragile" && c.CargoWeight < 1000))
                {
                    Console.WriteLine($"{car.Model}");
                }
            }
            else if (carsToPrint == "flamable")
            {
                foreach (Car car in carList.Where(c => c.CargoType == "flamable" && c.EnginePower > 250))
                {
                    Console.WriteLine($"{car.Model}");
                }
            }
        }
    }
}