using System;
using System.Collections.Generic;

namespace _07._Parking_Lot
{
    class Program
    {
        static void Main(string[] args)
        {
            string input;
            HashSet<string> carsOnTheParkingLot = new HashSet<string>();
            while ((input = Console.ReadLine()) != "END")
            {
                string[] info = input.Split(", ");
                string direction = info[0];
                string plateNumber = info[1];

                if (direction == "IN")
                {
                    carsOnTheParkingLot.Add(plateNumber);
                }
                else if (direction == "OUT")
                {
                    carsOnTheParkingLot.Remove(plateNumber);
                }
            }

            if (carsOnTheParkingLot.Count == 0)
            {
                Console.WriteLine("Parking Lot is Empty");
                return;
            }
            
            foreach (var plateNumber in carsOnTheParkingLot)
            {
                Console.WriteLine(plateNumber);
            }
        }
    }
}