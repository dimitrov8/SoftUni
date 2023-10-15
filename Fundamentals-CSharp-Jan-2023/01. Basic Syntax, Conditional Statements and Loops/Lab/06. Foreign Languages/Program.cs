namespace Foreign_Languages
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            string cName = Console.ReadLine();

            string language = "unknown";
            if (cName == "England" || cName == "USA")
            {
                language = "English";
            }
            else if (cName == "Spain" || cName == "Argentina" || cName == "Mexico")
            {
                language = "Spanish";
            }
           
            Console.WriteLine(language);
        }
    }
}
