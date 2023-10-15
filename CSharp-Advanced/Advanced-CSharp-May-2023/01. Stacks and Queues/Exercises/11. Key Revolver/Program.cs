using System;
using System.Collections.Generic;
using System.Linq;

namespace _11._Key_Revolver
{
    class Program
    {
        static void Main(string[] args)
        {
            int bulletPrice = int.Parse(Console.ReadLine()); // Price for a single bullet shot
            int gunBarrelSize = int.Parse(Console.ReadLine()); // Size of the gun barrel 
            var bullets = new Stack<int>(Console
                .ReadLine() // Each bullet is stored in a stack because we shoot from the last bullet put in the barrel
                .Split()
                .Select(int.Parse));

            var locks = new Queue<int>(Console
                .ReadLine() // Each lock is stored in a queue because we start shooting from the first lock
                .Split()
                .Select(int.Parse));

            int value = int.Parse(Console.ReadLine()); // The total value if we go through each lock
            int bulletsShot = 0; // Counter for each bullet we shot
            while (bullets.Any() && locks.Any()) // While we have bullets and we have locks
            {
                int currentBullet = bullets.Pop(); // Variable for the current bullet
                int currentLock = locks.Peek(); // Variable for the current lock
                bulletsShot++; // Increment the bulletsShot counter
                if (currentBullet <= currentLock) // If we can go through the lock
                {
                    Console.WriteLine("Bang!"); // Print 
                    locks.Dequeue(); // Remove the current lock
                }

                else if (currentBullet > currentLock) // If we can't get through the lock
                {
                    Console.WriteLine("Ping!"); // Print
                }

                if (bulletsShot % gunBarrelSize == 0 &&
                    bullets.Any()) // If our barrel is empty because it can hold specified number of bullets and we have bullets left
                {
                    Console.WriteLine("Reloading!"); // Print
                }
            }

            if (bullets.Count == 0 &&
                locks.Any()) // If we run out of bullets and we didn't go through all the locks successfully
            {
                Console.WriteLine($"Couldn't get through. Locks left: {locks.Count}"); // Print
            }
            else if (locks.Count == 0) // Else if we got through every lock successfully
            {
                int earnedMoney = value - (bulletPrice * bulletsShot); // Calculate the earned money
                Console.WriteLine($"{bullets.Count} bullets left. Earned ${earnedMoney}"); // Print
            }
        }
    }
}