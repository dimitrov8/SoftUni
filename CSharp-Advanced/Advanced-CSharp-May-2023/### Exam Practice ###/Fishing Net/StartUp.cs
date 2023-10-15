using System;

namespace FishingNet
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            Net net = new Net("cast net", 2);

            Fish fishOne = new Fish("salmon", 44.25, 941.15);
            Fish fishTwo = new Fish("catfish", 105.25, 2100.15);
            Fish fishThree = new Fish("bass", 25.25, 321.15);

            Console.WriteLine(net.AddFish(fishOne));            
            Console.WriteLine(net.AddFish(fishTwo));            
            Console.WriteLine(net.AddFish(fishThree));

            Console.WriteLine(net.ReleaseFish(2100.15));
            
            Console.WriteLine(net.Report());
         

        }
    }
}