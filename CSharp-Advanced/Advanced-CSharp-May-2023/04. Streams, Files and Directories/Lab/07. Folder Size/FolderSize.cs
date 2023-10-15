using System.Net;

namespace FolderSize
{
    using System;
    using System.IO;
    public class FolderSize
    {
        static void Main(string[] args)
        {
            string folderPath = @"..\..\..\Files\TestFolder";
            string outputPath = @"..\..\..\Files\output.txt";

            GetFolderSize(folderPath, outputPath);
        }

        public static void GetFolderSize(string folderPath, string outputFilePath)
        {
            double sizeInKB = 0;
            var dirs = new DirectoryInfo(folderPath);
            FileInfo[] files = dirs.GetFiles("*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                sizeInKB += file.Length;
            }

            sizeInKB /= 1024;

            File.WriteAllText(outputFilePath, $"{sizeInKB} KB");
        }
    }
}
