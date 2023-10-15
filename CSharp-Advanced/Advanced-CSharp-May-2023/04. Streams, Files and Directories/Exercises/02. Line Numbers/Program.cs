namespace LineNumbers
{
    using System.IO;
    using System.Linq;

    public class LineNumbers
    {
        static void Main()
        {
            string inputFilePath = @"..\..\..\text.txt";
            string outputFilePath = @"..\..\..\output.txt";

            ProcessLines(inputFilePath, outputFilePath);
        }

        public static void ProcessLines(string inputFilePath, string outputFilePath)
        {
            using (var reader = new StreamReader(inputFilePath))
            {
                using (var writer = new StreamWriter(outputFilePath))
                {
                    int line = 1;
                    while (!reader.EndOfStream)
                    {
                        string text = reader.ReadLine();
                        int nLetters = text.Count(char.IsLetter);
                        int nPunctuationMarks = text.Count(char.IsPunctuation);
                        writer.WriteLine($"Line {line++}: {text} ({nLetters}) ({nPunctuationMarks})");
                    }
                }
            }
        }
    }
}