namespace PlayCatch
{
    using System;
    using System.Linq;

    public class StartUp
    {
        private const string INDEX_DOES_NOT_EXIST_EXCEPTION = "The index does not exist!";
        private const string VARIABLE_IS_NOT_IN_THE_CORRECT_FORMAT_EXCEPTION = "The variable is not in the correct format!";

        private const string REPLACE_COMMAND = "Replace";
        private const string PRINT_COMMAND = "Print";
        private const string SHOW_COMMAND = "Show";

        static void Main(string[] args)
        {
            int[] numbers = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();
            
            int occuredExceptions = 0;
            while (occuredExceptions != 3)
            {
                try
                {
                    ManipulateArray(numbers);
                }
                catch (IndexOutOfRangeException iofr)
                {
                    occuredExceptions++;
                    Console.WriteLine(iofr.Message);
                }
                catch (FormatException fex)
                {
                    occuredExceptions++;
                    Console.WriteLine(fex.Message);
                }
            }

            Console.WriteLine(string.Join(", ", numbers));
        }

        private static void ManipulateArray(int[] numbers)
        {
            string[] args = Console.ReadLine()
                .Split();

            string mainCommand = args[0];
            if (mainCommand == REPLACE_COMMAND)
            {
                int index = CheckIfVariableIsInvalid(args[1]);

                CheckIfIndexIsOutOfRange(index, numbers);
                numbers[index] = int.Parse(args[2]);
            }
            else if (mainCommand == PRINT_COMMAND)
            {
                int startIndex = CheckIfVariableIsInvalid(args[1]);
                int endIndex = CheckIfVariableIsInvalid(args[2]);

                CheckIfIndexIsOutOfRange(startIndex, numbers);
                CheckIfIndexIsOutOfRange(endIndex, numbers);
                PrintElements(startIndex, endIndex, numbers);
            }
            else if (mainCommand == SHOW_COMMAND)
            {
                int elementAtIndex = CheckIfVariableIsInvalid(args[1]);
                
                CheckIfIndexIsOutOfRange(elementAtIndex, numbers);
                Console.WriteLine(numbers[elementAtIndex]);
            }
        }

        private static int CheckIfVariableIsInvalid(string value)
        {
            if (!int.TryParse(value, out int index))
                throw new FormatException(VARIABLE_IS_NOT_IN_THE_CORRECT_FORMAT_EXCEPTION);

            return index;
        }

        private static void CheckIfIndexIsOutOfRange(int index, int[] numbers)
        {
            if (index < 0 || index >= numbers.Length)
                throw new IndexOutOfRangeException(INDEX_DOES_NOT_EXIST_EXCEPTION);
        }

        private static void PrintElements(int startIndex, int endIndex, int[] numbers)
        {
            int[] newNumbersArray = new int[endIndex];
            Array.Copy(numbers, startIndex, newNumbersArray, 0, endIndex);
            Console.WriteLine(string.Join(", ", newNumbersArray));
        }
    }
}