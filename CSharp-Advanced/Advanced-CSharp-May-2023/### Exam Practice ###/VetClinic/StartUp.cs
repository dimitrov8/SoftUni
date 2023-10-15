using System;

namespace VetClinic
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            Clinic clinic = new Clinic(20);

            Pet dog = new Pet("Ellias", 5, "Tim");

            Console.WriteLine(dog);

            clinic.Add(dog);

            Console.WriteLine(clinic.Remove("Ellias")); 
            Console.WriteLine(clinic.Remove("Pufa"));

            Pet cat = new Pet("Bella", 2, "Mia");
            Pet bunny = new Pet("Zak", 4, "Jon");

            clinic.Add(cat);
            clinic.Add(bunny);

            Pet oldestPet = clinic.GetOldestPet();
            Console.WriteLine(oldestPet);

            Pet pet = clinic.GetPet("Bella", "Mia");
            Console.WriteLine(pet);

            Console.WriteLine(clinic.Count);

            Console.WriteLine(clinic.GetStatistics());
        }
    }
}