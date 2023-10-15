namespace SplitMergeBinaryFile
{
    using System.IO;
    using System.Linq;

    public class SplitMergeBinaryFile
    {
        static void Main()
        {
            string sourceFilePath = @"..\..\..\Files\example.png";
            string joinedFilePath = @"..\..\..\Files\example-joined.png";
            string partOnePath = @"..\..\..\Files\part-1.bin";
            string partTwoPath = @"..\..\..\Files\part-2.bin";

            SplitBinaryFile(sourceFilePath, partOnePath, partTwoPath);
            MergeBinaryFiles(partOnePath, partTwoPath, joinedFilePath);
        }

        public static void SplitBinaryFile(string sourceFilePath, string partOneFilePath, string partTwoFilePath)
        {
            using (var examplePNG = new FileStream(sourceFilePath, FileMode.Open))
            {
                using (var firstPartOutput = new FileStream(partOneFilePath, FileMode.Create))
                {
                    byte[] bufferFirstPart = new byte[examplePNG.Length / 2 + 1];
                    examplePNG.Read(bufferFirstPart, 0, bufferFirstPart.Length);
                    firstPartOutput.Write(bufferFirstPart.ToArray(), 0, bufferFirstPart.Length);
                }

                using (var secondPartOutput = new FileStream(partTwoFilePath, FileMode.Create))
                {
                    byte[] bufferSecondPart = new byte[examplePNG.Length / 2];
                    examplePNG.Read(bufferSecondPart, 0, bufferSecondPart.Length);
                    secondPartOutput.Write(bufferSecondPart.ToArray(), 0, bufferSecondPart.Length);
                }
            }
        }

        public static void MergeBinaryFiles(string partOneFilePath, string partTwoFilePath, string joinedFilePath)
        {
            using (var joined = new FileStream(joinedFilePath, FileMode.Create))
            {
                using (var firstPart = new FileStream(partOneFilePath, FileMode.Open))
                {
                    byte[] firstBytes = new byte[firstPart.Length];
                    firstPart.Read(firstBytes, 0, firstBytes.Length);
                    joined.Write(firstBytes, 0, firstBytes.Length);
                }

                using (var secondPart = new FileStream(partTwoFilePath, FileMode.Open))
                {
                    byte[] secondBytes = new byte[secondPart.Length];
                    secondPart.Read(secondBytes, 0, secondBytes.Length);
                    joined.Write(secondBytes, 0, secondBytes.Length);
                }
            }
        }
    }
}