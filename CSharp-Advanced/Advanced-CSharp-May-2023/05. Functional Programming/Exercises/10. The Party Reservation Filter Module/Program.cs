using System;
using System.Collections.Generic;
using System.Linq;

namespace _10._Party_Reservation_Filter_Module
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> invitations = Console.ReadLine().Split().ToList(); // The invitations list
            List<string> filters = new List<string>(); // Store each filter
            string command;
            while ((command = Console.ReadLine()) != "Print") // While the command is not "Print"
            {
                string[] tokens = command.Split(';', StringSplitOptions.RemoveEmptyEntries);
                string mainCommand = tokens[0]; // Main command (Add filter/Remove filter)
                string filterType = tokens[1]; // Filter type (Starts with/Ends with/Length/Contains)
                string parameter = tokens[2]; // Parameter (Starts with/Ends With/Contains: string) - (Length: int.parse)
                
                if (mainCommand == "Add filter") 
                {
                    filters.Add($"{filterType};{parameter}");
                }
                else if (mainCommand == "Remove filter")
                {
                    filters.Remove($"{filterType};{parameter}");
                }
            }

            // Functions
            Func<string, string, bool> startsWithFilter = (name, parameter) => name.StartsWith(parameter);
            Func<string, string, bool> endsWithFilter = (name, parameter) => name.EndsWith(parameter);
            Func<string, int, bool> lengthFilter = (name, length) => name.Length == length;
            Func<string, string, bool> containsFilter = (name, parameter) => name.Contains(parameter);

            foreach (var filter in filters) // For each filter in the list of filters
            {
                string[] filterTokens = filter.Split(';'); // Split 
                string filterType = filterTokens[0]; // Filter type
                string parameter = filterTokens[1]; // Parameter

                switch (filterType) // Switch filter type
                {
                    case "Starts with": invitations = invitations.Where(n => !startsWithFilter(n, parameter)).ToList(); break;
                    case "Ends with": invitations = invitations.Where(n => !endsWithFilter(n, parameter)).ToList(); break;
                    case "Length": 
                        int length = int.Parse(parameter); 
                        invitations = invitations.Where(n => !lengthFilter(n, length)).ToList(); 
                        break;
                    case "Contains": invitations = invitations.Where(n => !containsFilter(n, parameter)).ToList(); break;
                }
            }

            Console.WriteLine(string.Join(" ", invitations)); // Print
        }
    }
}

