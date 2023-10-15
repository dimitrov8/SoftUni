namespace EvenLines
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class EvenLines
    {
        static void Main()
        {
            string inputFilePath = @"..\..\..\text.txt";

            Console.WriteLine(ProcessLines(inputFilePath));
        }

        public static string ProcessLines(string inputFilePath)
        {
            StringBuilder sb = new StringBuilder();
            using (var reader = new StreamReader(inputFilePath))
            {
                int lineNumber = 0;
                while (!reader.EndOfStream)
                {
                    string currentLineText = reader.ReadLine();

                    if (lineNumber % 2 == 0)
                    {
                        foreach (var symbol in currentLineText.Where(char.IsPunctuation))
                        {
                            if (symbol == '-' || symbol == ',' || symbol == '.' || symbol == '!' || symbol == '?')
                            {
                                currentLineText = currentLineText.Replace(symbol, '@');
                            }
                        }

                        if (lineNumber != 0)
                        {
                            sb.AppendLine();
                        }

                        string[] words = currentLineText.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                        for (int i = words.Length - 1; i >= 0; i--)
                        {
                            sb.Append(words[i]);
                            if (i != 0)
                            {
                                sb.Append(' ');
                            }
                        }
                    }
                    lineNumber++;
                }
            }

            return sb.ToString();
        }
    }
}