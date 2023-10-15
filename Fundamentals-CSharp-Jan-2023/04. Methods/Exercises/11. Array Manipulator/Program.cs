namespace P11.ArrayManipulator
{
    using System;
    using System.Linq;

    internal class Program
    {
        static void Main(string[] args)
        {
            // Our original array input
            int[] arr = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            // Variable for the command
            string command;

            // While our command is not "end" go in =>
            while ((command = Console.ReadLine()) != "end")
            {
                // Create an array so we can split our commands
                // Split them by empty space
                string[] cmdArgs = command.Split(" ");

                // Our command type is at index 0
                string cmdType = cmdArgs[0];

                // If command is "exchange" =>
                if (cmdType == "exchange")
                {
                    // Get by how many indexes we want to exchange
                    int index = int.Parse(cmdArgs[1]);

                    // If the input index is lower than 0 or it exceeds our original array
                    if (index < 0 || index >= arr.Length)
                    {
                        // We print "Invalid index"
                        Console.WriteLine("Invalid index");
                        // And continue so we can get new command
                        continue;
                    }

                    // Go in our ExchangeArray Method and store the return of it in the original array
                    arr = ExchangeArray(arr, index);
                }
                // If command is "max" || "min" =>
                else if (cmdType == "max" || cmdType == "min")
                {
                    // Our second input in the command is "even" or "odd"
                    // Which is at index 1
                    string evenOrOddArg = cmdArgs[1];

                    // Variable to store the return the index
                    // The return result of the method
                    int indexOfEl = -1;

                    // If command is "max"
                    if (cmdType == "max")
                    {
                        // Go in the IndexOfMaxEvenOrOddElement Method
                        indexOfEl = IndexOfMaxEvenOrOddElement(arr, evenOrOddArg);
                    }
                    // If command is min
                    else if (cmdType == "min")
                    {
                        indexOfEl = IndexOfMinEvenOrOddElement(arr, evenOrOddArg);
                    }

                    // Check if our indexOfEl has it's default value of -1 
                    // If it's -1 it means there was no matching element
                    // Print:
                    // And get new command input
                    if (indexOfEl == -1)
                    {
                        Console.WriteLine("No matches");
                        continue;
                    }

                    // Else if index is found
                    Console.WriteLine(indexOfEl);
                }
                else if (cmdType == "first" || cmdType == "last")
                {
                    // Our first index indicates how many numbers we should return
                    int count = int.Parse(cmdArgs[1]);

                    // Our second index indicates if we should return even or odd elements
                    string evenOrOddArg = cmdArgs[2];

                    // If input count exceeds our array 
                    if (count > arr.Length)
                    {
                        // Print 
                        Console.WriteLine("Invalid count");
                        // Continue to get the next command
                        continue;
                    }

                    // Create new array in which we store the first/last even/odd elements
                    int[] elements = new int[count];

                    // If command is "first"
                    if (cmdType == "first")
                    {
                        elements = FirstEvenOrOddElements(arr, count, evenOrOddArg);
                    }

                    // If command is "last"
                    else if (cmdType == "last")
                    {
                        elements = LastEvenOrOddElements(arr, count, evenOrOddArg);
                    }

                    PrintArray(elements);
                }
            }

            PrintArray(arr);
        }

        // 
        static int IndexOfMinEvenOrOddElement(int[] arr, string evenOrOddArg)
        {
            int minIndex = -1;
            int currMin = int.MaxValue;

            for (int i = 0; i < arr.Length; i++)
            {
                int currN = arr[i];

                if (evenOrOddArg == "even" && currN % 2 == 0)
                {
                    if (currN <= currMin)
                    {
                        currMin = currN;
                        minIndex = i;
                    }
                }
                else if (evenOrOddArg == "odd" && currN % 2 != 0)
                {
                    if (currN <= currMin)
                    {
                        currMin = currN;
                        minIndex = i;
                    }
                }
            }

            return minIndex;
        }


        static void PrintArray(int[] arr)
        {
            Console.WriteLine($"[{string.Join(", ", arr)}]");
        }

        static int[] LastEvenOrOddElements(int[] arr, int count, string evenOrOddArg)
        {
            int[] firstElArr = new int[count];
            int firstElIndex = 0;

            for (int i = arr.Length - 1; i >= 0; i--)
            {
                if (firstElIndex >= count)
                {
                    break;
                }

                int currN = arr[i];

                if (evenOrOddArg == "even" && currN % 2 == 0)
                {
                    firstElArr[firstElIndex++] = currN;
                }
                else if (evenOrOddArg == "odd" && currN % 2 != 0)
                {
                    firstElArr[firstElIndex++] = currN;
                }
            }

            if (firstElIndex < count)
            {
                firstElArr = ResizeArray(firstElArr, firstElIndex);
            }

            firstElArr = ReverseArray(firstElArr);

            return firstElArr;
        }

        // Method to get the first even or odd elements
        static int[] FirstEvenOrOddElements(int[] arr, int count, string evenOrOddArg)
        {
            // Create new array to store the first elements. It has the length of the input count
            int[] firstElArr = new int[count];

            // Create an index to start from and add 1 to it every loop
            int firstElIndex = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                // If at any point our firstIndexEl is greater or equal to our input count. 
                // It means we filled the array
                if (firstElIndex >= count)
                {
                    // Break.
                    break;
                }

                // Create variable for the currentN 
                int currN = arr[i];

                // If our 3rd argument is "even" and the number is even: 
                if (evenOrOddArg == "even" && currN % 2 == 0)
                {
                    // Add the current number from the original array to the new firstElArr
                    firstElArr[firstElIndex++] = arr[i];
                }

                // If our 3rd argument is "odd" and the number is odd:
                else if (evenOrOddArg == "odd" && currN % 2 != 0)
                {
                    // Add the current number from the original array to the new firstElArr
                    firstElArr[firstElIndex++] = arr[i];
                }
            }

            // If our count input is greater than our firstElIndex
            if (firstElIndex < count)
            {
                firstElArr = ResizeArray(firstElArr, firstElIndex);
            }

            // Return the firstEl array
            return firstElArr;
        }

        // Method to reverse the array
        static int[] ReverseArray(int[] firstElArr)
        {
            // Create new reversed array which has the length of the firstElArr
            int[] reversed = new int[firstElArr.Length];
            for (int i = firstElArr.Length - 1; i >= 0; i--)
            {
                reversed[reversed.Length - i - 1] = firstElArr[i];
            }

            // Return reversed array
            return reversed;
        }

        // Method to resize the array 
        static int[] ResizeArray(int[] firstElArr, int firstElIndex)
        {
            // Create new modified empty array which is the length of firstElIndex 
            int[] modifiedArray = new int[firstElIndex];

            // Loop from 0 to the firstElIndex
            for (int i = 0; i < firstElIndex; i++)
            {
                // Store the firstElArr[i] to the modifiedArr[i]
                modifiedArray[i] = firstElArr[i];
            }

            // Return the modified array
            return modifiedArray;
        }

        // Returns the index of the max even or odd element if found
        // If there is no matching element it returns -1
        static int IndexOfMaxEvenOrOddElement(int[] arr, string evenOrOddArg)
        {
            // Default value for the maxIndex which means there is no match
            int maxIndex = -1;

            // Store the max Index there
            int currMax = int.MinValue;

            // Loop to find: Max number index in the array
            for (int i = 0; i < arr.Length; i++)
            {
                // Our current number 
                int currNum = arr[i];

                // If the second argument is "even": and our currN is even =>
                if (evenOrOddArg == "even" && currNum % 2 == 0)
                {
                    // If our currN >= currMax: =>
                    // It's >= because we need to get the rightmost one 
                    if (currNum >= currMax)
                    {
                        // Store the currN in the currMax
                        currMax = currNum;
                        // Store the current index in the maxIndex
                        maxIndex = i;
                    }
                }
                // If the second argument is "odd" and our currN is odd: =>
                else if (evenOrOddArg == "odd" && currNum % 2 != 0)
                {
                    // Do the same as above:
                    // If our currN >= currMax: =>
                    // It's >= because we need to get the rightmost one 
                    if (currNum >= currMax)
                    {
                        // Store the currN in the currMax
                        currMax = currNum;
                        // Store the current index in the maxIndex
                        maxIndex = i;
                    }
                }
            }

            // Return the maxIndex
            return maxIndex;
        }


        // Method for the "exchange" command: 
        private static int[] ExchangeArray(int[] arr, int index)
        {
            // 1. Create a new empty modified array which is the same length as the original array
            int[] modifiedArr = new int[arr.Length];
            // 2. Create modifiedArrIndex so know on which index to store the data
            int modifiedArrIndex = 0;

            // 3. Create for loop.
            // => Starting from our input index + 1
            // ==> Because indexes start from 0.
            // ===> And we need to start from the index after the input index.
            for (int i = index + 1; i < arr.Length; i++)
            {
                // : Lets say our input index is 2
                // --- So we start from index 3
                // ---- Store the index 3 of the original array to the index 0
                // ----- Of the modified array.
                // ------ Do the same for every index...
                modifiedArr[modifiedArrIndex++] = arr[i];
            }

            // 2.After that we need one more for loop to fill our entire
            // Modified array (The right side).
            // => So we start from 0 to our input index.
            // ==> Because that way we store the missed digits.
            // ===> Because our first For loop starts from index + 1
            // ====> And only loop to the input index so we don't override
            // : our upper for loop
            for (int i = 0; i <= index; i++)
            {
                modifiedArr[modifiedArrIndex++] = arr[i];
            }

            // Return our modifiedArr
            return modifiedArr;
        }
    }
}