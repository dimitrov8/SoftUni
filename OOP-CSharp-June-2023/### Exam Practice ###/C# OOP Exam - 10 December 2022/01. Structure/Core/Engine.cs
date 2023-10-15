namespace ChristmasPastryShop.Core
{
    using Contracts;
    using IO;
    using IO.Contracts;
    using System;

    public class Engine : IEngine
    {
        private readonly IWriter writer;
        private readonly IReader reader;
        private readonly IController controller;

        public Engine()
        {
            this.writer = new Writer();
            this.reader = new Reader();
            this.controller = new Controller();
        }

        public void Run()
        {
            while (true)
            {
                string[] input = this.reader.ReadLine().Split();
                if (input[0] == "Exit")
                {
                    Environment.Exit(0);
                }

                try
                {
                    string result = string.Empty;

                    if (input[0] == "AddBooth")
                    {
                        int capacity = int.Parse(input[1]);

                        result = this.controller.AddBooth(capacity);
                    }
                    else if (input[0] == "AddDelicacy")
                    {
                        int boothId = int.Parse(input[1]);
                        string delicacyTypeName = input[2];
                        string delicacyName = input[3];

                        result = this.controller.AddDelicacy(boothId, delicacyTypeName, delicacyName);
                    }
                    else if (input[0] == "AddCocktail")
                    {
                        int boothId = int.Parse(input[1]);
                        string coctailTypeName = input[2];
                        string cocktailName = input[3];
                        string size = input[4];

                        result = this.controller.AddCocktail(boothId, coctailTypeName, cocktailName, size);
                    }
                    else if (input[0] == "ReserveBooth")
                    {
                        int countOfPeople = int.Parse(input[1]);

                        result = this.controller.ReserveBooth(countOfPeople);
                    }
                    else if (input[0] == "TryOrder")
                    {
                        int bootId = int.Parse(input[1]);
                        string order = input[2];

                        result = this.controller.TryOrder(bootId, order);
                    }
                    else if (input[0] == "LeaveBooth")
                    {
                        int boothId = int.Parse(input[1]);

                        result = this.controller.LeaveBooth(boothId);
                    }
                    else if (input[0] == "BoothReport")
                    {
                        int boothId = int.Parse(input[1]);

                        result = this.controller.BoothReport(boothId);
                    }

                    this.writer.WriteLine(result);
                }
                catch (Exception ex)
                {
                    this.writer.WriteLine(ex.Message);
                }
            }
        }
    }
}
