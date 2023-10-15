using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace _03._Speed_Racing
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            List<Car> carList = new List<Car>();
            for (int i = 1; i <= n; i++)
            {
                string[] carInfo = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string currModel = carInfo[0];
                double currFuelAmount = Double.Parse(carInfo[1]);
                double currFuelConsumption = Double.Parse(carInfo[2]);
                int kmTraveled = 0;

                carList.Add(new Car(currModel, currFuelAmount, currFuelConsumption, kmTraveled));
            }

            string input;
            while ((input = Console.ReadLine()) != "End")
            {
                string[] inputArgs = input.Split(" ");
                string carToDrive = inputArgs[1];
                int distanceToDrive = int.Parse(inputArgs[2]);

                Car currCar = carList.First(c => c.Model == carToDrive);
                if (distanceToDrive * currCar.FuelConsumption <= currCar.FuelAmount)
                {
                    currCar.KmTraveled += distanceToDrive;
                    currCar.FuelAmount -= distanceToDrive * currCar.FuelConsumption;
                }

                else
                {
                    Console.WriteLine("Insufficient fuel for the drive");
                }
            }

            foreach (Car car in carList)
            {
                Console.WriteLine(car.ToString());
            }
        }
    }

    public class Car
    {
        public string Model { get; set; }
        public double FuelAmount { get; set; }
        public double FuelConsumption { get; set; }

        public int KmTraveled { get; set; }

        public Car(string currModel, double currFuelAmount, double currFuelConsumption, int kmTraveled)
        {
            Model = currModel;
            FuelAmount = currFuelAmount;
            FuelConsumption = currFuelConsumption;
            KmTraveled = kmTraveled;
        }

        public override string ToString()
        {
            return $"{Model} {FuelAmount:F2} {KmTraveled}";
        }
    }
}