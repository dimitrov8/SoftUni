namespace Data_Type_Finder
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            string input;

            while ((input = Console.ReadLine()) != "END")
            {
                string type = string.Empty;
                if (int.TryParse(input, out  _))
                {
                    type = "is integer type";
                }
                else if (double.TryParse(input, out _))
                {
                    type = "is floating point type";
                }
                else if (char.TryParse(input, out _))
                {
                    type = "is character type";
                }
                else if (bool.TryParse(input, out _))
                {
                    type = "is boolean type";
                }
                else
                {
                    type = "is string type";
                }
                Console.WriteLine($"{input} {type}");
            }
        }
    }
}
