using System;
using System.Collections.Generic;
using System.Linq;

namespace _03._Articles_2._0
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Article> articles = new List<Article>();
            int nArticlesOutputs = int.Parse(Console.ReadLine());
            for (int i = 1; i <= nArticlesOutputs; i++)
            {
                string[] input = Console.ReadLine().Split(",").Select(x => x.Trim()).ToArray();
                Article articleObj = new Article(input[0], input[1], input[2]);
                articles.Add(articleObj);
            }

            Console.WriteLine(string.Join(Environment.NewLine, articles));
        }
    }

    public class Article
    {
        private string Title { get; set; }
        private string Content { get; set; }
        private string Author { get; set; }

        // Declare the constructor for the class
        public Article(string title, string content, string author)
        {
            Title = title;
            Content = content;
            Author = author;
        }

        public override string ToString()
        {
            return $"{Title} - {Content}: {Author}";
        }
    }
}