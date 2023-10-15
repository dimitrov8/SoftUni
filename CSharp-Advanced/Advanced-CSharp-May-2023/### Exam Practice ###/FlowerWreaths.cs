using System;
using System.Collections.Generic;
using System.Linq;

namespace FlowerWreaths
{
    class Program
    {
        static void Main(string[] args)
        {
            var lilies = new Stackint(Console.ReadLine()!
                .Split(, , StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray());
            var roses = new Queueint(Console.ReadLine()!
                .Split(, , StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray());

            int wreathCount = 0;
            int flowersLeft = 0;

            while (lilies.Any() && roses.Any())
            {
                int sum = lilies.Pop() + roses.Dequeue();
                if (sum == 15)
                {
                    wreathCount++;
                }
                else if (sum  15)
                {
                    while (sum  15) sum -= 2;

                    if (sum  15) flowersLeft += sum;

                    else wreathCount++;
                }

                else flowersLeft += sum;

                if (wreathCount == 5) break;
            }

            Console.WriteLine(wreathCount == 5
                 You made it, you are going to the competition with 5 wreaths!
                 $You didn't make it, you need {5 - (flowersLeft  15 + wreathCount)} wreaths more!);
        }
    }
}
