using System;
using System.Collections.Generic;

namespace _10._Crossroads
{
    class Program
    {
        static void Main(string[] args)
        {
            int greenLightDuration = int.Parse(Console.ReadLine());
            int yellowLightDuration = int.Parse(Console.ReadLine());

            Queue<string> cars = new Queue<string>(); // Queue of cars
            int passedCarsCount = 0; // Counter used to count the number of passed cars
            string command; 
            while ((command = Console.ReadLine()) != "END") // While loop unit "END"
            {
                if (command == "green") // If the command is "green"
                {
                    int remainingSeconds = greenLightDuration; // Remaining green light duration (keep in mind that we have yellow light duration)
                    while (remainingSeconds > 0 && cars.Count > 0) // While the remaining second are greater than 0 and there are remaining cars
                    {
                        string currentCar = cars.Peek(); // Take the current car
                        if (currentCar.Length <= remainingSeconds + yellowLightDuration) // If the car length is less than the remaining seconds
                        {
                            remainingSeconds -= cars.Dequeue().Length; // The remaining seconds are decreased and the car passes successfully
                        }
                        else if (currentCar.Length > remainingSeconds + yellowLightDuration) // If the car cant pass even with the extra time because of the yellow light
                        {
                            Console.WriteLine("A crash happened!"); // Print
                            Console.WriteLine($"{currentCar} was hit at {currentCar[remainingSeconds + yellowLightDuration]}."); // Print the car and the char which was hit. It's indicated by the remainingSeconds + yellowLightDuration
                            return; // Stop the program
                        }

                        passedCarsCount++; // If a car passed successfully then increment the passedCarsCount
                    }
                }
                else if (command != "green") // If the command is not "green"
                {
                    cars.Enqueue(command); // Enqueue car
                }
            }

            // If every car passed 
            Console.WriteLine("Everyone is safe."); // Print 
            Console.WriteLine($"{passedCarsCount} total cars passed the crossroads."); // Print
        }
    }
}