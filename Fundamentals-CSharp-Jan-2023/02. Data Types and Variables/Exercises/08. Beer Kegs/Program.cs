using System;

namespace Beer_Kegs
{
    class Program
    {
        static void Main(string[] args)
        {
            //Input 
            int numberOfKegs = int.Parse(Console.ReadLine());

            float max = float.MinValue;
            string biggestKeg = string.Empty;

            for (int i = 1; i <= numberOfKegs; i++)
            {
                string model = Console.ReadLine();
                float radius = float.Parse(Console.ReadLine());
                int height = int.Parse(Console.ReadLine());

                float volume = MathF.PI * MathF.Pow(radius, 2) * height;
                if (volume > max)
                {
                    max = volume;
                    biggestKeg = model;
                }
            }
            Console.WriteLine(biggestKeg);
        }
    }
}
