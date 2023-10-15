namespace CommandPattern.Core
{
    using Contracts;
    using System;

    public class Engine : IEngine
    {
        private readonly ICommandInterpreter commandInterpreter;

        public Engine(ICommandInterpreter commandInterpreter)
        {
            this.commandInterpreter = commandInterpreter;
        }

        public void Run()
        {
            string input;
            while ((input = Console.ReadLine()) != "Exit")
            {
                string result = this.commandInterpreter.Read(input);
                Console.WriteLine(result);
            }
        }
    }
}