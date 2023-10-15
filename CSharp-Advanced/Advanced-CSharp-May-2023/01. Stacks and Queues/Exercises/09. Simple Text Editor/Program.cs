using System;
using System.Collections.Generic;
using System.Text;

namespace _09._Simple_Text_Editor
{
    class Program
    {
        static void Main(string[] args)
        {
            int nOfCommands = int.Parse(Console.ReadLine());
            StringBuilder text = new StringBuilder();
            Stack<string> stack = new Stack<string>();
            for (int i = 0; i < nOfCommands; i++)
            {
                string[] cmdArgs = Console.ReadLine().Split();
                string mainCommand = cmdArgs[0];

                if (mainCommand == "1") // 1 someString - appends someString to the end of the text
                {
                    stack.Push(text.ToString()); // Push the variation of the text into the stack before th appending
                    text.Append(cmdArgs[1]); // Append the text with the user input
                }
                else if (mainCommand == "2") // 2 count - erases the last count elements from the text
                {
                    stack.Push(text.ToString()); // Push the variation of the text into the stack before
                    text.Remove(text.Length - int.Parse(cmdArgs[1]), int.Parse(cmdArgs[1])); // Remove the last count elements from the text
                }
                else if (mainCommand == "3") // 3 index - returns the element at position index from the text
                {
                    char letter = text[int.Parse(cmdArgs[1]) - 1]; // Take the letter from the text at the desired index
                    Console.WriteLine(letter);  // Print the letter at the desired index
                }
                else if (mainCommand == "4") //  4 - undoes the last not undone command of type 1 / 2 and returns the text to the state before that operation
                {
                    text = new StringBuilder(stack.Pop()); // Revert to the last change 
                }
            }
        }
    }
}