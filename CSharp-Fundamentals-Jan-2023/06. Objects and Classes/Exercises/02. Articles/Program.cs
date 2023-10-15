using System;
using System.Linq;

namespace _02._Articles
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] article = Console.ReadLine().Split(",").Select(x=> x.Trim()).ToArray();
            string title = article[0];
            string content = article[1];
            string author = article[2];

            Article articleObj = new Article(title, content, author);
            int nCommands = int.Parse(Console.ReadLine());
            for (int i = 1; i <= nCommands; i++)
            {
                string[] cmdArgs = Console.ReadLine().Split(":");
                string cmdType = cmdArgs[0].Trim();
                string replace = cmdArgs[1].Trim();

                articleObj.ArticleCommand(cmdType, replace);
            }

            Console.WriteLine(articleObj.ToString());
        }
    }

    public class Article
    {
        private string Title { get; set; }
        private string Content { get; set; }
        private string Author { get; set; }

        public Article(string title, string content, string author)
        {
            Title = title;
            Content = content;
            Author = author;
        }

        public void ArticleCommand(string cmdType, string replace)
        {
            if (cmdType == "Edit")
            {
                Content = replace;
            }
            else if (cmdType == "ChangeAuthor")
            {
                Author = replace;
            }
            else if (cmdType == "Rename")
            {
                Title = replace;
            }
        }

        public override string ToString()
        {
            return $"{Title} - {Content}: {Author}";
        }
    }
}