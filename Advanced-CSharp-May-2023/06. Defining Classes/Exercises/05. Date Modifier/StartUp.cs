using System;

namespace DefiningClasses
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            string startDate = Console.ReadLine();
            string endDate = Console.ReadLine();
            
            Console.WriteLine(new DateModifier(startDate, endDate).DaysDifference());
        }
    }
}