using System;

namespace Lower_Or_Upper
{
    class Program
    {
        static void Main(string[] args)
        {
            char input = char.Parse(Console.ReadLine());


            if (input >= 97 && input <= 122)    
            {
                Console.WriteLine("lower-case");
            }
            else if(input >= 65 && input <= 90)
            {
                Console.WriteLine("upper-case");
            }
        }
    }
}
