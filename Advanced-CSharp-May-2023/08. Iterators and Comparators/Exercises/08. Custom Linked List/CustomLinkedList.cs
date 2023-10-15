using System;

namespace CustomLinkedList
{
    public class CustomLinkedList<T>
    {
        private ListNode<T> head; // Reference to the head (first node) of the linked list
        private ListNode<T> tail; // Reference to the tail (last node) of the linked list

        public int Count { get; private set; } // Count of the elements in the linked list

        public void Add(T value) // Add method
        {
            var newNode = new ListNode<T>(value); // Declare a new node to be added
            if (this.head == null) // If the head is null, the linked list is empty
            {
                this.head = this.tail = newNode; // The new node becomes both the head and the tail
            }
            else // If the head is not null, the linked list is not empty
            {
                this.tail.Next = this.tail = newNode; // 1. Connect the current tail to the new node 2.// Update the tail to be the new node
                this.tail.Next = null; // // Set the Next reference of the new tail to null
            }

            this.Count++;
        }

        public void Remove(T value) // Remove method 
        {
            ListNode<T> currentNode = this.head; // Start from the head
            ListNode<T> previousNode = null; // Reference to the previous node

            while (currentNode != null) // While the linked list is not empty
            {
                if (currentNode.Value.Equals(value))  // If the current node's value matches the desired value to remove
                {
                    if (previousNode != null) // If the previous node is not null (current node is not the head)
                    {
                        previousNode.Next = currentNode.Next;  // Remove the link between the current node and the previous node
                        if (currentNode.Equals(this.tail)) // If the current node is the tail
                        {
                            this.tail = previousNode; // Update the tail to be the previous node
                        }
                    }
                    else // If the previous node is null (current node is the head)
                    {
                        this.head = currentNode.Next; // Update the head to be the next node
                    }

                    this.Count--; // Decrease the count of elements in the linked list
                    return; // Exit the method if the desired item is removed
                }

                previousNode = currentNode; // Update the previous node to be the current node
                currentNode = currentNode.Next; // Move to the next node in the iteration
            }
        }

        public bool Contains(T value) // Contains method
        {
            ListNode<T> currentNode = this.head; // Start from the head
            while (currentNode != null) // While the current node is not null
            {
                if (currentNode.Value.Equals(value)) // If the current node's value matches the desired value
                {
                    return true; // The value is found in the linked list
                }

                currentNode = currentNode.Next; // Move to the next node in the iteration
            }

            return false; // The value is not found in the linked list
        }

        public T[] ToArray()
        {
            T[] array = new T[this.Count]; // Create a new array with the size of the linked list
            ListNode<T> currentNode = this.head; // Start from the head

            for (int i = 0; i < this.Count; i++) // Iterate over the linked list
            {
                array[i] = currentNode.Value; // Store the value of the current node in the array
                currentNode = currentNode.Next; // Move to the next node
            }

            return array; // Return the array containing the values of the linked list
        }

        public void ForEach(Action<T> action) // ForEach method
        {
            ListNode<T> currentNode = this.head; // Start from the head

            while (currentNode != null) // Iterate over the linked list
            {
                action(currentNode.Value); // Perform the specified action on the value of the current node
                currentNode = currentNode.Next; // Move to the next node
            }
        }

        private class ListNode<T>
        {
            public T Value { get; set; } // Value of the node
            public ListNode<T> Next { get; set; } // Reference to the next node
            
            public ListNode(T value)
            {
                this.Value = value; // Set the value of the node
                this.Next = null; // Initialize the next reference to null
            }
        }
    }
}