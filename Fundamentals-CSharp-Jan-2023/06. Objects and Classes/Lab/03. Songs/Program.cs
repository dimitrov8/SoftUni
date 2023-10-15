using System;
using System.Collections.Generic;
using System.Linq;

namespace _03._Songs
{
    class Program
    {
        static void Main(string[] args)
        {
            int nSongs = int.Parse(Console.ReadLine());

            List<Song> songs = new List<Song>();
            for (int i = 1; i <= nSongs; i++)
            {
                string[] currSong = Console.ReadLine()
                    .Split("_")
                    .ToArray();
                string typeList = currSong[0];
                string name = currSong[1];
                string time = currSong[2];

                Song song = new Song(typeList, name, time);
                songs.Add(song);
            }

            string list = Console.ReadLine();

            for (int i = 0; i < songs.Count; i++)
            {
                Song currSong = songs[i];

                if (list == "all")
                {
                    Console.WriteLine(currSong.Name);
                }
                else if (list == currSong.TypeList)
                {
                    Console.WriteLine(currSong.Name);
                }
            }
        }
    }

    class Song
    {
        public Song(string typeList, string name, string time)
        {
            TypeList = typeList;
            Name = name;
            Time = time;
        }

        public string TypeList { get; set; }

        public string Name { get; set; }

        private string Time { get; set; }
    }
}