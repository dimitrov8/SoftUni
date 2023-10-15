using System;
using System.Collections.Generic;
using System.Linq;

namespace _06._Vehicle_Catalogue
{
    class Program
    {
        static void Main(string[] args)
        {
            string input;
            List<VehicleInfo> carList = new List<VehicleInfo>();
            List<VehicleInfo> truckList = new List<VehicleInfo>();
            while ((input = Console.ReadLine()) != "End")
            {
                string[] vehicleData = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string vehicleType = vehicleData[0];
                // Make the first letter uppercase car -> Car
                vehicleType = char.ToUpper(vehicleType[0]) + vehicleType.Substring(1);
                string vehicleModel = vehicleData[1];
                string vehicleColor = vehicleData[2];
                double vehicleHp = double.Parse(vehicleData[3]);

                VehicleInfo vehicle = new VehicleInfo(vehicleType, vehicleModel, vehicleColor, vehicleHp);
                if (vehicleType == "Car")
                    carList.Add(vehicle);

                else if (vehicleType == "Truck")
                    truckList.Add(vehicle);
            }

            string getInfo;
            while ((getInfo = Console.ReadLine()) != "Close the Catalogue")
            {
                List<VehicleInfo> allVehicles = carList.Concat(truckList).ToList();
                bool exists = allVehicles.Any(v => v.Model == getInfo);
                if (exists)
                {
                    VehicleInfo currVehicleData = allVehicles.Find(v => v.Model == getInfo);
                    Console.WriteLine($"Type: {currVehicleData.Type}");
                    Console.WriteLine($"Model: {currVehicleData.Model}");
                    Console.WriteLine($"Color: {currVehicleData.Color}");
                    Console.WriteLine($"Horsepower: {currVehicleData.HorsePower}");
                }
            }
            double carAvgHp = CalculateAverageHp(carList, truckList, out double truckAvgHp);

            Console.WriteLine($"Cars have average horsepower of: {carAvgHp:F2}.");
            Console.WriteLine($"Trucks have average horsepower of: {truckAvgHp:F2}.");
        }

        private static double CalculateAverageHp(List<VehicleInfo> carList, List<VehicleInfo> truckList,
            out double truckAvgHp)
        {
            double carAvgHp = carList.Sum(car => car.HorsePower) / carList.Count;
            truckAvgHp = truckList.Sum(truck => truck.HorsePower) / truckList.Count;

            if (carList.Count == 0)
                carAvgHp = 0;
            if (truckList.Count == 0)
                truckAvgHp = 0;

            return carAvgHp;
        }

        private class VehicleInfo
        {
            public string Type { get; set; }
            public string Model { get; set; }
            public string Color { get; set; }
            public double HorsePower { get; set; }

            public VehicleInfo(string type, string model, string color, double horsePower)
            {
                Type = type;
                Model = model;
                Color = color;
                HorsePower = horsePower;
            }
        }
    }
}