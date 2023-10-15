using System;
using System.Linq;

namespace CustomLinkedList
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomLinkedList<int> linkedList = new CustomLinkedList<int>();
            linkedList.Add(10); // Adds 10 to the linked list
            linkedList.Add(20); // Adds 20 to the linked list
            linkedList.Add(30); // Adds 30 to the linked list
            linkedList.Remove(20); // Remove 20 from the linked list

            Console.WriteLine($"Count of elements in the linked list: {linkedList.Count}"); // Prints the count of elements in the linked list

            Console.WriteLine($"Does the linked list contain 30? => {linkedList.Contains(30)}"); // Checks if the linked list contains the element '30' which will return True
            Console.WriteLine($"Does the linked list contain 40? => {linkedList.Contains(40)}"); // Checks if the linked list contains the element '40' which will return False

            int[] array = linkedList.ToArray(); // Calls our ToArray method and the array will look like this => [10, 30] 
            Console.WriteLine($"Elements in the array: {string.Join(", ", array.Select(e => $"[{e}]"))}"); // Print every element in the array
            int sumOfElements = 0; // Initialize the variable for the sum of elements
            linkedList.ForEach(x => sumOfElements += x); // Use the ForEach method we created to calculate the sum of elements
            Console.WriteLine($"The sum of all elements in the array is: {sumOfElements}"); // Returns the sum of all elements in the array
        }
    }
}

