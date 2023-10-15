namespace BookingApp.Core
{
    using Contracts;
    using IO;
    using IO.Contracts;
    using System;
    using System.Text;

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

                    if (input[0] == "AddHotel")
                    {
                        var sb = new StringBuilder();
                        int category = int.Parse(input[input.Length - 1]);

                        for (int i = 1; i < input.Length - 1; i++)
                        {
                            sb.Append(input[i] + ' ');
                        }

                        string hotelName = sb.ToString().TrimEnd();
                        result = this.controller.AddHotel(hotelName, category);
                    }
                    else if (input[0] == "UploadRoomTypes")
                    {
                        var hotelName = new StringBuilder();
                        int indexOfType = input.Length - 1;
                        string roomType = input[indexOfType];

                        for (int i = 1; i < indexOfType; i++)
                        {
                            hotelName.Append(input[i] + ' ');
                        }


                        result = this.controller.UploadRoomTypes(hotelName.ToString().TrimEnd(), roomType);
                    }
                    else if (input[0] == "SetRoomPrices")
                    {
                        var hotelName = new StringBuilder();
                        string roomType = input[^2];
                        double price = double.Parse(input[^1]);

                        for (int i = 1; i < input.Length - 2; i++)
                        {
                            hotelName.Append(input[i] + ' ');
                        }

                        result = this.controller.SetRoomPrices(hotelName.ToString().TrimEnd(), roomType, price);
                    }
                    else if (input[0] == "BookAvailableRoom")
                    {
                        int adults = int.Parse(input[1]);
                        int children = int.Parse(input[2]);
                        int duration = int.Parse(input[3]);
                        int category = int.Parse(input[4]);

                        result = this.controller.BookAvailableRoom(adults, children, duration, category);
                    }
                    else if (input[0] == "HotelReport")
                    {
                        var hotelName = new StringBuilder();
                        for (int i = 1; i < input.Length; i++)
                        {
                            hotelName.Append(input[i] + ' ');
                        }

                        result = this.controller.HotelReport(hotelName.ToString().TrimEnd());
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