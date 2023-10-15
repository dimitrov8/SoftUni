using System;

namespace NeedForSpeed
{
    public class Motorcycle : Vehicle
    {
        public Motorcycle(int horsePower, double fuel) : base(horsePower, fuel)
        {
        }

        public override void Drive(double kilometers)
        {
            Console.WriteLine("Riding a motorcycle...");
            base.Drive(kilometers);
        }
    }
}