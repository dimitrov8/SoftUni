using static BinarySearch;

int[] array = Console.ReadLine()!.Split().Select(int.Parse).ToArray();

Console.WriteLine(IndexOf(array, int.Parse(Console.ReadLine()!)));

public class BinarySearch
{
    public static int IndexOf(int[] array, int key)
    {
        int low = 0;
        int high = array.Length - 1;
        while (low <= high) // Continue the search as long as the lower bound is less than or equal to the upper bound
        {
            int mid = low + (high - low) / 2; // Calculate the middle index of the current search range

            if (key == array[mid]) return mid; // If the key is found: => Return the index

            // Narrow the search range
            low = key < array[mid] ? low : mid + 1;
            high = key > array[mid] ? high : mid - 1;
        }

        return -1; // If the key is not found, return -1
    }
}
