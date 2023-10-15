using System;
using System.Collections.Generic;

namespace _05._Cities_by_Continent_and_Country
{
    class Program
    {
        static void Main(string[] args)
        {
            int nOfInputs = int.Parse(Console.ReadLine());
            var dict = new Dictionary<string, Dictionary<string, List<string>>>();

            for (int i = 0; i < nOfInputs; i++)
            {
                string[] inputData = Console.ReadLine().Split();
                string continentName = inputData[0];
                string country = inputData[1];
                string city = inputData[2];

                if (!dict.ContainsKey(continentName)) 
                {
                    dict.Add(continentName, new Dictionary<string, List<string>>() { { country, new List<string> { city } } });
                }
                else if (dict.ContainsKey(continentName) && !dict[continentName].ContainsKey(country))
                {
                    dict[continentName].Add(country, new List<string>() { city });
                }
                else if (dict.ContainsKey(continentName) && dict[continentName].ContainsKey(country))
                {
                    dict[continentName][country].Add(city);
                }
            }

            foreach (var continent in dict.Keys)
            {
                Console.WriteLine($"{continent}:");
                foreach (var countryData in dict[continent])
                {
                    Console.WriteLine($"  {countryData.Key} -> {string.Join(", ", countryData.Value)}");
                }
            }
            
        }
    }
}