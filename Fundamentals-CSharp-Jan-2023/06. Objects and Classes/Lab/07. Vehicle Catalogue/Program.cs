using System;
using System.Collections.Generic;
using System.Linq;

namespace _07._Vehicle_Catalogue
{
    class Program
    {
        static void Main(string[] args)
        {
            string input;

            List<Car> cars = new List<Car>();
            List<Truck> trucks = new List<Truck>();
            Catalog catalog = new Catalog();
            while ((input = Console.ReadLine()) != "end")
            {
                string[] vehicleData = input.Split("/");
                string type = vehicleData[0];
                string brand = vehicleData[1];
                string model = vehicleData[2];
                string value = vehicleData[3];

                if (type == "Car")
                {
                    Car car = new Car(brand, model, value);
                    catalog.Cars.Add(car);
                }
                else if (type == "Truck")
                {
                    Truck truck = new Truck(brand, model, value);
                    catalog.Trucks.Add(truck);
                }
            }
            catalog.PrintCatalog();
        }
    }

    public class Truck
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Weight { get; set; }

        public Truck(string brand, string model, string weight)
        {
            Brand = brand;
            Model = model;
            Weight = weight;
        }
    }

    public class Car
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string HorsePower { get; set; }

        public Car(string brand, string model, string value)
        {
            Brand = brand;
            Model = model;
            HorsePower = value;
        }
    }

    public class Catalog
    {
        public List<Car> Cars { get; set; }
        public List<Truck> Trucks { get; set; }

        public Catalog()
        {
            Cars = new List<Car>();
            Trucks = new List<Truck>();
        }

        public void PrintCatalog()
        {
            Console.WriteLine("Cars:");
            foreach (Car car in Cars.OrderBy(x => x.Brand))
            {
                Console.WriteLine($"{car.Brand}: {car.Model} - {car.HorsePower}hp");
            }

            Console.WriteLine("Trucks:");
            foreach (Truck truck in Trucks.OrderBy(x => x.Brand))
            {
                Console.WriteLine($"{truck.Brand}: {truck.Model} - {truck.Weight}kg");
            }
        }
    }
}