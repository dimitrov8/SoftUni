namespace NavalVessels.IO
{
    using Contracts;
    using System.IO;

    public class TextWriter : IWriter
    {
        readonly string path = "../../../output.txt";

        public TextWriter()
        {
            // Clear the file's content when the TextReader is constructed
            using var writer = new StreamWriter(this.path, false);
            writer.Write("");
        }

        public void Write(string message)
        {
            // Use FileMode.Append to add content to the file
            using var writer = new StreamWriter(this.path, true);
            writer.Write(message);
        }

        public void WriteLine(string message)
        {
            // Check if the message is not empty before writing it to the file
            if (!string.IsNullOrEmpty(message))
            {
                // Use FileMode.Append to add content to the file
                using var writer = new StreamWriter(this.path, true);
                writer.WriteLine(message);
            }
        }
    }
}