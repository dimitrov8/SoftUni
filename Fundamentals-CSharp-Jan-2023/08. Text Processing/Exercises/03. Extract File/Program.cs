using System;
using System.Linq;

namespace _03._Extract_File
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = Console.ReadLine().Split("\\").ToArray();
            string[] fileInfo = input[^1].Split('.').ToArray();
            Console.WriteLine($"File name: {fileInfo[0]}");
            Console.WriteLine($"File extension: {fileInfo[1]}");
        }
    }
}