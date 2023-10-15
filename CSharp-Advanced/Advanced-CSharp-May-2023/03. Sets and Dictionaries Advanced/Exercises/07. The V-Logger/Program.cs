using System;
using System.Collections.Generic;
using System.Linq;

namespace _07._The_V_Logger
{
    class Program
    {
        static void Main(string[] args)
        {
            string input;
            var data = new Dictionary<string, Vlogger>();
            while ((input = Console.ReadLine()) != "Statistics")
            {
                string[] tokens = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string vlogger = tokens[0];
                string command = tokens[1];

                if (command == "joined" && !data.ContainsKey(vlogger))
                {
                    data.Add(vlogger, new Vlogger(0, 0));
                }
                else if (command == "followed" && data.ContainsKey(vlogger) && data.ContainsKey(tokens[2]) && tokens[2] != vlogger && data[tokens[2]].FollowersNames.All(f => f != vlogger))
                {
                    Vlogger followingVlogger = data[vlogger];
                    Vlogger followedVlogger = data[tokens[2]];
                    followingVlogger.Following++;

                    followedVlogger.Followers++;
                    followedVlogger.FollowersNames.Add(tokens[0]);
                }
            }

            Console.WriteLine($"The V-Logger has a total of {data.Keys.Count} vloggers in its logs.");
            int n = 1;
            foreach (var kvp in data.OrderByDescending(v => data[v.Key].Followers).ThenBy(v => data[v.Key].Following))
            {
                Console.WriteLine($"{n}. {kvp.Key} : {data[kvp.Key].Followers} followers, {data[kvp.Key].Following} following");
                if (n == 1)
                {
                    foreach (var follower in data[kvp.Key].FollowersNames.OrderBy(f => f))
                    {
                        Console.WriteLine($"*  {follower}");
                    }
                }

                n++;
            }

            Console.WriteLine();
        }
    }

    public class Vlogger
    {
        public int Followers { get; set; }
        public int Following { get; set; }
        public List<string> FollowersNames { get; set; }

        public Vlogger(int followers, int following)
        {
            Followers = followers;
            Following = following;
            FollowersNames = new List<string>();
        }
    }
}