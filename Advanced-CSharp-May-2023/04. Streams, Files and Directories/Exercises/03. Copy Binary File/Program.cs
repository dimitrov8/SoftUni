namespace CopyBinaryFile
{
    using System.Collections.Generic;
    using System.IO;

    public class CopyBinaryFile
    {
        static void Main()
        {
            string inputFilePath = @"..\..\..\copyMe.png";
            string outputFilePath = @"..\..\..\copyMe-copy.png";

            CopyFile(inputFilePath, outputFilePath);
        }

        public static void CopyFile(string inputFilePath, string outputFilePath)
        {
            List<byte> bytes = new List<byte>();
            using (var stream = new FileStream(inputFilePath, FileMode.Open))
            {
                using (var output = new FileStream(outputFilePath, FileMode.Create))
                {
                    byte[] buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);
                    foreach (var @byte in buffer)
                    {
                        bytes.Add(@byte);
                    }

                    output.Write(bytes.ToArray(), 0, buffer.Length);
                }
            }
        }
    }
}