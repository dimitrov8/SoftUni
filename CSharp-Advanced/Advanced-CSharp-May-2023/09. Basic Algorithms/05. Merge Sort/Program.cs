// Read the input from the console
int[] array = Console.ReadLine()!
    .Split()
    .Select(int.Parse)
    .ToArray();

// Sort the array using the Mergesort algorithm.
Mergesort<int>.Sort(array);

// Print the sorted array elements, separated by a space.
Console.WriteLine(string.Join(" ", array));

public class Mergesort<T> where T : IComparable
{
    private static T[]? aux;  // Auxiliary array used for merging segments during the sorting process.

    private static void Merge(T[] arr, int lo, int mid, int hi)
    {
        if (arr[mid].CompareTo(arr[mid + 1]) <= 0)
        {
            // Arrays are already sorted, no need to merge
            return;
        }

        // Copy values to the auxiliary array
        Array.Copy(arr, lo, aux!, lo, hi - lo + 1);

        int leftIndex = lo; // Current index in the left segment
        int rightIndex = mid + 1; // Current index in the right segment
        int mergeIndex = lo; // Current index in the merged array

        // Merge the divided segments
        while (leftIndex <= mid && rightIndex <= hi)
        {
            if (aux![leftIndex].CompareTo(aux[rightIndex]) <= 0)
            {
                // The element in the left segment is smaller or equal
                // Copy it to the merged array and move to the next element in the left segment
                arr[mergeIndex] = aux[leftIndex];
                leftIndex++;
            }
            else
            {
                // The element in the right segment is smaller
                // Copy it to the merged array and move to the next element in the right segment
                arr[mergeIndex] = aux[rightIndex];
                rightIndex++;
            }

            mergeIndex++; // Move to the next position in the merged array
        }

        while (leftIndex <= mid)
        {
            // Copy any remaining elements from the left segment
            arr[mergeIndex] = aux![leftIndex];
            mergeIndex++;
            leftIndex++;
        }

        while (rightIndex <= hi)
        {
            // Copy any remaining elements from the right segment
            arr[mergeIndex] = aux![rightIndex];
            mergeIndex++;
            rightIndex++;
        }
    }

    public static void Sort(T[] arr)
    {
        aux = new T[arr.Length]; // Initialize the auxiliary array with the same length as the input array
        Sort(arr, 0, arr.Length - 1); // Start the recursive sorting process by sorting the entire array
        aux = null; // Clear the auxiliary array after sorting
    }

    private static void Sort(T[] arr, int lo, int hi)
    {
        if (lo >= hi) return; // Base case: array segment is empty or contains only one element, no need to sort further

        int mid = lo + (hi - lo) / 2; // Calculate the middle index of the current segment

        Sort(arr, lo, mid); // Sort the left segment recursively
        Sort(arr, mid + 1, hi); // Sort the right segment recursively
        Merge(arr, lo, mid, hi); // Merge the sorted left and right segments
    }
}
