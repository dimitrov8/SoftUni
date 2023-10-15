using System;
using System.Collections.Generic;
using System.Linq;

namespace DefiningClasses
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            string command;
            HashSet<Trainer> allTrainers = new HashSet<Trainer>();
            while ((command = Console.ReadLine()) != "Tournament")
            {
                string[] tokens = command.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string trainerName = tokens[0];
                string pokemonName = tokens[1];
                string pokemonElement = tokens[2];
                int pokemonHealth = int.Parse(tokens[3]);

                Trainer currentTrainer = new Trainer(trainerName);
                allTrainers.Add(currentTrainer);
                Pokemon pokemonCaught = new Pokemon(pokemonName, pokemonElement, pokemonHealth);
                allTrainers.First(x => x.Name == trainerName).Caught(pokemonCaught);

            }

            while ((command = Console.ReadLine()) != "End")
            {
                string desiredElement = command;

                foreach (var trainer in allTrainers)
                {
                    if (trainer.PokemonCollection.Any(x => x.ElementName == desiredElement))
                    {
                        trainer.BadgesCount++;
                    }
                    else
                    {
                        trainer.Damage();
                    }
                }                
            }

            foreach (var trainer in allTrainers.OrderByDescending(x => x.BadgesCount))
            {
                Console.WriteLine(trainer);
            }
        }
    }
}