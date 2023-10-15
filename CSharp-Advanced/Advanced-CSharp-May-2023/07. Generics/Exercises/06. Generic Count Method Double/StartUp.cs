using System;

namespace GenericCountMethodDoubles
{
   public class StartUp
    {
        static void Main(string[] args)
        {
            int nOfElements = int.Parse(Console.ReadLine()!);
            Box<double> box = new Box<double>();
            for (int i = 0; i < nOfElements; i++)
            {
                box.Add(double.Parse(Console.ReadLine()!));
            }

            if (box.ItemsCount > 0)
            {
                Console.WriteLine(box.Count(double.Parse(Console.ReadLine()!)));
            }
        }
    }
}