using System;

namespace ExtractSpecialBytes
{
    using System.IO;
    using System.Collections.Generic;
    public class ExtractSpecialBytes
    {
        static void Main()
        {
            string binaryFilePath = @"..\..\..\Files\example.png";
            string bytesFilePath = @"..\..\..\Files\bytes.txt";
            string outputPath = @"..\..\..\Files\output.bin";

            ExtractBytesFromBinaryFile(binaryFilePath, bytesFilePath, outputPath);
        }

        public static void ExtractBytesFromBinaryFile(string binaryFilePath, string bytesFilePath, string outputPath)
        {
            List<byte> bytes = new List<byte>();
            using (var bytesReader = new StreamReader(bytesFilePath))
            {
                while (!bytesReader.EndOfStream)
                {
                    bytes.Add(byte.Parse(bytesReader.ReadLine()));
                }
            }
            
            List<byte> commonBytes = new List<byte>();
            using (var pngStream = new FileStream(binaryFilePath, FileMode.Open))
            {
                using (var writer = new FileStream(outputPath, FileMode.Create))
                {
                    byte[] bytesBuffer = new byte[pngStream.Length];
                    pngStream.Read(bytesBuffer, 0, bytesBuffer.Length);
                    foreach (var currentByte in bytesBuffer)
                    {
                        if (bytes.Contains(currentByte))
                        {
                            commonBytes.Add(currentByte);
                        }
                    }
                    writer.Write(commonBytes.ToArray(), 0, commonBytes.Count);
                }
            }
            
        }
    }
}
