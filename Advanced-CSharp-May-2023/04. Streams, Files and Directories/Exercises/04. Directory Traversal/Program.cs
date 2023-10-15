using System.Net;

namespace DirectoryTraversal
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class DirectoryTraversal
    {
        static void Main()
        {
            string path = Console.ReadLine();
            string reportFileName = @"\report.txt";

            string reportContent = TraverseDirectory(path);
            Console.WriteLine(reportContent);

            WriteReportToDesktop(reportContent, reportFileName);
        }

        public static string TraverseDirectory(string inputFolderPath)
        {
            var dir = new DirectoryInfo(inputFolderPath);

            FileInfo[] files = dir.GetFiles("*", SearchOption.TopDirectoryOnly);
            var filesByExtensions = new Dictionary<string, Dictionary<string, long>>();

            foreach (var file in files)
            {
                string extension = file.Extension;
                string fileName = file.Name;
                long size = file.Length / 1024;

                if (!filesByExtensions.ContainsKey(extension))
                {
                    filesByExtensions.Add(extension, new Dictionary<string, long>());
                }

                filesByExtensions[extension].Add(fileName, size);
            }

            StringBuilder sb = new StringBuilder();
            foreach (var kvp in filesByExtensions
                         .OrderByDescending(c => c.Value.Count)
                         .ThenBy(c => c.Key))
            {
                string extension = kvp.Key;
                foreach (var f in kvp.Value.OrderBy(f => f.Value))
                {
                    sb.Append($"-- {f.Key} - {f.Value} kb");
                    sb.AppendLine();
                }
            }

            return sb.ToString();
        }

        public static void WriteReportToDesktop(string textContent, string reportFileName)
        {
            string pathToCreate = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            pathToCreate += reportFileName;
            
            File.WriteAllText(pathToCreate, textContent);
        }
    }
}

