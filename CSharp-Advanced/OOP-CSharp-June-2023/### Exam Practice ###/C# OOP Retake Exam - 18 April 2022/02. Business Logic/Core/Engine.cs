namespace Heroes.Core
{
    using Contracts;
    using IO;
    using IO.Contracts;
    using System;

    public class Engine : IEngine
    {
        private readonly IWriter writer;
        private readonly IReader reader;
        private readonly IController controller;

        public Engine()
        {
            this.writer = new Writer();
            this.reader = new Reader();
            this.controller = new Controller();
        }

        public void Run()
        {
            while (true)
            {
                string[] input = this.reader.ReadLine().Split();
                if (input[0] == "Exit")
                {
                    Environment.Exit(0);
                }

                try
                {
                    string result = string.Empty;

                    if (input[0] == "CreateHero")
                    {
                        string type = input[1];
                        string name = input[2];
                        int health = int.Parse(input[3]);
                        int armour = int.Parse(input[4]);

                        result = this.controller.CreateHero(type, name, health, armour);
                    }
                    else if (input[0] == "CreateWeapon")
                    {
                        string weaponType = input[1];
                        string name = input[2];
                        int durability = int.Parse(input[3]);

                        result = this.controller.CreateWeapon(weaponType, name, durability);
                    }
                    else if (input[0] == "AddWeaponToHero")
                    {
                        string weaponName = input[1];
                        string heroName = input[2];

                        result = this.controller.AddWeaponToHero(weaponName, heroName);
                    }
                    else if (input[0] == "StartBattle")
                    {
                        result = this.controller.StartBattle();
                    }
                    else if (input[0] == "HeroReport")
                    {
                        result = this.controller.HeroReport();
                    }

                    this.writer.WriteLine(result);
                }
                catch (Exception ex)
                {
                    this.writer.WriteLine(ex.Message);
                }
            }
        }
    }
}