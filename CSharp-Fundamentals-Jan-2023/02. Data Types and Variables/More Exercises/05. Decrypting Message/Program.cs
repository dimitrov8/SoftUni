namespace Decrypting_Messages
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            int key = int.Parse(Console.ReadLine());
            int rows = int.Parse(Console.ReadLine());

            string[] arr = new string[rows];
            for (int i = 0; i < rows; i++)
            {
                char curr = char.Parse(Console.ReadLine()); 
                int letter = curr + key;
                
                char ch = (char)letter;
                arr[i] += ch;
            }
            Console.WriteLine(string.Join("", arr));
        }
    }
}
