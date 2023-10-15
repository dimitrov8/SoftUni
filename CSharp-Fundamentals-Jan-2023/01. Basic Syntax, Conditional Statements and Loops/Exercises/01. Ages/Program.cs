namespace Ages
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            int age = int.Parse(Console.ReadLine());

            string text = string.Empty;
            if (age >= 0 && age <= 2)
            {
                text = "baby";
            }
            else if (age >= 3 && age <= 13)
            {
                text = "child";
            }
            else if (age >= 14 && age <= 19)
            {
                text = "teenager";
            }
            else if (age >= 20 && age <= 65)
            {
                text = "adult";
            }
            else if (age >= 66)
            {
                text = "elder";
            }
            Console.WriteLine(text);
        }
    }
}
