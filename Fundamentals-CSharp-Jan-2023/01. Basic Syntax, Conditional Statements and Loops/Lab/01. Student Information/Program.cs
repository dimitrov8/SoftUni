namespace Student_Information
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            string studenName = Console.ReadLine();
            int age = int.Parse(Console.ReadLine());
            double averageGrade = double.Parse(Console.ReadLine());

            Console.WriteLine($"Name: {studenName}, Age: {age}, Grade: {averageGrade:f2}");
        }
    }
}
