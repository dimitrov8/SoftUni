namespace ZipAndExtract
{
    using System.IO;
    using System.IO.Compression;
    
    public class ZipAndExtract
    {
        static void Main()
        {
            string inputFile = @"..\..\..\copyMe.png";
            string zipArchiveFile = @"..\..\..\archive.zip";
            string extractedFile = @"..\..\..\extracted.png";

            ZipFileToArchive(inputFile, zipArchiveFile);

            var fileNameOnly = Path.GetFileName(inputFile);
            ExtractFileFromArchive(zipArchiveFile, fileNameOnly, extractedFile);
        }

        public static void ZipFileToArchive(string inputFilePath, string zipArchiveFilePath)
        {
            var fileName = Path.GetFileName(inputFilePath); 
            using (var zip = ZipFile.Open(zipArchiveFilePath, ZipArchiveMode.Create)) 
            {
                zip.CreateEntryFromFile(inputFilePath, fileName);
            }
        }

        public static void ExtractFileFromArchive(string zipArchiveFilePath, string fileName, string outputFilePath)
        {
            using (var zip = ZipFile.Open(zipArchiveFilePath, ZipArchiveMode.Read))
            {
                zip.GetEntry(fileName).ExtractToFile(outputFilePath);
            }
        }
    }
}