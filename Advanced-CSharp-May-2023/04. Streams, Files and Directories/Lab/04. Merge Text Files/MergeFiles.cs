namespace MergeFiles
{
    using System;
    using System.IO;

    public class MergeFiles
    {
        static void Main()
        {
            var firstInputFilePath = @"..\..\..\Files\input1.txt";
            var secondInputFilePath = @"..\..\..\Files\input2.txt";
            var outputFilePath = @"..\..\..\Files\output.txt";

            MergeTextFiles(firstInputFilePath, secondInputFilePath, outputFilePath);
        }

        public static void MergeTextFiles(string firstInputFilePath, string secondInputFilePath, string outputFilePath)
        {
            using (var firstReader = new StreamReader(firstInputFilePath))
            {
                using (var secondReader = new StreamReader(secondInputFilePath))
                {
                    int firstReaderLength = firstReader.ReadToEnd().Split(Environment.NewLine).Length;
                    int secondReaderLength = secondReader.ReadToEnd().Split(Environment.NewLine).Length;
                    int maxLength = Math.Max(firstReaderLength, secondReaderLength);
                    int minLength = Math.Min(firstReaderLength, secondReaderLength);
                    firstReader.BaseStream.Position = 0;
                    secondReader.BaseStream.Position = 0;
                    using (var writer = new StreamWriter(outputFilePath))
                        for (int i = 0; i < maxLength; i++)
                        {
                            if (i < minLength)
                            {
                                writer.WriteLine(firstReader.ReadLine());
                                writer.WriteLine(secondReader.ReadLine());
                            }
                            else
                            {
                                if (!firstReader.EndOfStream)
                                {
                                    writer.WriteLine(firstReader.ReadLine());
                                }
                                else if (!secondReader.EndOfStream)
                                {
                                    writer.WriteLine(secondReader.ReadLine());
                                }
                            }
                        }
                }
            }
        }
    }
}