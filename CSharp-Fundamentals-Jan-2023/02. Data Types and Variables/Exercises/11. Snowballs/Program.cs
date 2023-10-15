using System;
using System.Numerics;

namespace Snowballs
{
    class Program
    {
        static void Main(string[] args)
        {
            byte snowballs = byte.Parse(Console.ReadLine()); // n (number of snowballs)

            BigInteger max = 0; // Store the best snowBallValue
            byte maxSnowData = 0; // Store maxSnowData
            ushort maxSnowballTimeData = 0; // Store maxSnowballTimeData
            byte maxSnowballQualityData = 0; // Store maxSnowballQualityData

            for (byte b = 1; b <= snowballs; b++) // Loop for every snowball => We receive data about the snowball
            {
                byte snowdata = byte.Parse(Console.ReadLine());
                ushort snowballTimeData = ushort.Parse(Console.ReadLine());
                byte snowballQualityData = byte.Parse(Console.ReadLine());

                // Check the snowballValume on each loop
                BigInteger snowballValue = BigInteger.Pow(snowdata / snowballTimeData, snowballQualityData);

                // If the snowballValue is bigger than our current max(snowballValue) => store all data about that snowball
                if (snowballValue > max)
                {
                    max = snowballValue;
                    maxSnowData = snowdata;
                    maxSnowballTimeData = snowballTimeData;
                    maxSnowballQualityData = snowballQualityData;
                }
            }
            Console.WriteLine($"{maxSnowData} : {maxSnowballTimeData} = {max} ({maxSnowballQualityData})");
        }
    }
}
