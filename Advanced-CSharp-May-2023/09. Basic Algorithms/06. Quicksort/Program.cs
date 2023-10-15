using static QuickSort;

// Read input
int[] array = Console.ReadLine()!.Split().Select(int.Parse).ToArray();

// Shuffle the array
Shuffle(array);

// Sort the array using the QuickSort algorithm
Sort(array, 0, array.Length - 1);

// Print the sorted array
Console.WriteLine(string.Join(" ", array));

public class QuickSort
{
    public static void Shuffle<T>(T[] array)
    {
        Random random = new Random();
        for (int currentIndex = 0; currentIndex < array.Length; currentIndex++)
        {
            int randomIndex = random.Next(currentIndex, array.Length - 1); // Generate a random index between currentIndex and the last index
            Swap(array, currentIndex, randomIndex); // Swap the elements at currentIndex and randomIndex
        }
    }

    public static void Sort<T>(T[] array, int low, int high) where T : IComparable<T>
    {
        // Base case: 
        if (low >= high) return; // If the array has 0 or 1 element, it is already sorted

        // Perform partitioning and get the updated index of the pivot
        int index = Partition(array, low, high);

        // Recursively call QuickSort for the left and right partitions
        Sort(array, low, index - 1); // Sort the left partition
        Sort(array, index + 1, high); // Sort the right partition
    }

    private static int Partition<T>(T[] array, int low, int high) where T : IComparable<T>
    {
        if (low >= high) return low;  // If there is only one element, it is already partitioned and the index of the pivot is the index of its only element: => Return the index of the pivot

        T pivot = array[low]; // Choose the first element as the pivot
        int leftIndex = low + 1;
        int rightIndex = high;

        while (true)
        {
            while (leftIndex <= rightIndex && array[leftIndex].CompareTo(pivot) < 0)
            {
                leftIndex++; // Move the left index to the right until an element greater than or equal to the pivot is found
            }

            while (rightIndex >= leftIndex && array[rightIndex].CompareTo(pivot) > 0)
            {
                rightIndex--; // Move the right index to the left until an element smaller than or equal to the pivot is found
            }

            if (leftIndex > rightIndex) break; // If the left and right indices have crossed, exit the loop

            Swap(array, leftIndex, rightIndex); // Swap the elements at the left and right indices
            leftIndex++; // Move the left index to the right 
            rightIndex--; // Move the right index to the left
        }

        Swap(array, low, rightIndex); // Move the pivot to its final sorted position
        return rightIndex; // Return the updated index of the pivot
    }

    private static void Swap<T>(T[] array, int index1, int index2)
    {
        (array[index1], array[index2]) = (array[index2], array[index1]); // Swap the elements at index1 and index2 using tuple deconstruction
    }
}