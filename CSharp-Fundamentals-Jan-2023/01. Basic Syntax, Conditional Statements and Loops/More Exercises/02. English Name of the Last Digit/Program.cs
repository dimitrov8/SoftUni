namespace English_Name_of_the_Last_Digit
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine()) % 10;
            string word = string.Empty;
            switch (n)
            {
                case 0: word = "zero"; break;
                case 1: word = "one"; break;
                case 2: word = "two"; break;
                case 3: word = "three"; break;
                case 4: word = "four"; break;
                case 5: word = "five"; break;
                case 6: word = "six"; break;
                case 7: word = "seven"; break;
                case 8: word = "eight"; break;
                case 9: word = "nine"; break;
            }
            Console.WriteLine(word);
        }
    }
}
