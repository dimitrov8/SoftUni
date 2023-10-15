using System;

namespace _01._Advertisement_Message
{
    class Program
    {
        static void Main(string[] args)
        {
            Advertisemen ad = new Advertisemen();
            int nMessages = int.Parse(Console.ReadLine());

            for (int i = 0; i <= nMessages; i++)
            {
                Console.WriteLine(ad.Generate());
            }
        }
    }

    public class Advertisemen
    {
        private Random rnd = new Random();
        private string[] phrases =
        {
            "Excellent product.", "Such a great product.", "I always use that product.",
            "Best product of its category.", "Exceptional product.", "I can't live without this product."
        };
        
        private string[] events =
        {
            "Now I feel good.", "I have succeeded with this product.", "Makes miracles. I am happy of the results!",
            "I cannot believe but now I feel awesome.", "Try it yourself, I am very satisfied.", "I feel great!"
        };

        private string[] authors = { "Diana", "Petya", "Stella", "Elena", "Katya", "Iva", "Annie", "Eva" };

        private string[] cities = { "Burgas", "Sofia", "Plovdiv", "Varna", "Ruse" };


        public string Generate()
        {
            string phrase = phrases[rnd.Next(phrases.Length)];
            string ev = events[rnd.Next(events.Length)];
            string author = authors[rnd.Next(authors.Length)];
            string city = cities[rnd.Next(cities.Length)];

            return $"{phrase} {ev} {author} - {city}.";
        }
    }
}