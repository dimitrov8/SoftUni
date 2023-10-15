namespace CopyDirectory
{
    using System;
    using System.IO;

    public class CopyDirectory
    {
        static void Main()
        {
            string inputPath =  @$"{Console.ReadLine()}";
            string outputPath = @$"{Console.ReadLine()}";

            CopyAllFiles(inputPath, outputPath);
        }

        public static void CopyAllFiles(string inputPath, string outputPath)
        {
            foreach (var dirPath in Directory.GetDirectories(inputPath, "*", SearchOption.TopDirectoryOnly))
            {
                Directory.CreateDirectory(dirPath.Replace(inputPath, outputPath));
            }

            foreach (var output in Directory.GetFiles(inputPath, "*", SearchOption.TopDirectoryOnly))
            {
                File.Copy(inputPath, outputPath, true);
            }
        }
    }
}