namespace Chronometer;

public class StartUp
{
    static void Main(string[] args)
    {
        var chronometer = new Chronometer();
        Console.WriteLine("Welcome to the Chronometer App!");
        Console.WriteLine("Available commands: ");
        Console.WriteLine("   - start: " + Colorize("Start the chronometer", ConsoleColor.Green));
        Console.WriteLine("   - stop: " + Colorize("Stop the chronometer", ConsoleColor.Red));
        Console.WriteLine("   - lap: " + Colorize("Record a lap time", ConsoleColor.Cyan));
        Console.WriteLine("   - laps: View the list of lap times");
        Console.WriteLine("   - reset: " + Colorize("Reset the chronometer", ConsoleColor.Yellow));
        Console.WriteLine("   - time: " + Colorize("Get the current time", ConsoleColor.Blue));
        Console.ResetColor();
        Console.WriteLine("   - exit: Exit the application");
        Console.WriteLine("Type your command and press Enter.");


        string command;

        while ((command = Console.ReadLine()!) != "exit")
        {
            if (command == "start")
            {
                chronometer.Start();
            }
            else if (command == "stop")
            {
                chronometer.Stop();
            }
            else if (command == "lap")
            {
                Console.WriteLine(chronometer.Lap());
            }
            else if (command == "laps")
            {
                if (chronometer.Laps.Count == 0)
                {
                    Console.WriteLine("Laps: no laps");
                    continue;
                }

                Console.WriteLine("Laps: ");

                for (int i = 1; i <= chronometer.Laps.Count; i++)
                {
                    Console.WriteLine($"{i}. {chronometer.Laps[i - 1]}");
                }
            }
            else if (command == "reset")
            {
                chronometer.Reset();
            }
            else if (command == "time")
            {
                Console.WriteLine(chronometer.GetTime);
            }
            else
            {
                Console.WriteLine("Invalid command. Available commands: start, stop, lap, laps, reset, time, exit");
            }
        }

        chronometer.Stop();
    }

    private static string Colorize(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;

        return message;
    }
}