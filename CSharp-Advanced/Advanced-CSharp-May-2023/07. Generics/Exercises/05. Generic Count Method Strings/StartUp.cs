using System;

namespace GenericCountMethodStrings
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            int nOfElements = int.Parse(Console.ReadLine()!);
            Box<string> box = new Box<string>();
            for (int i = 0; i < nOfElements; i++)
            {
                box.Add(Console.ReadLine());
            }
            
            if (box.ItemsCount > 0)
            {
                Console.WriteLine(box.Count(Console.ReadLine()));
            }
        }
    }
}