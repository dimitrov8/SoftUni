namespace P01.Stream_Progress.Streams
{
    using Contracts;

    public class StreamProgressInfo
    {
        private readonly IStreamable file;

        public StreamProgressInfo(IStreamable file)
        {
            this.file = file;
        }

        public int CalculateCurrentPercent()
        {
            return this.file.BytesSent * 100 / this.file.Length;
        }
    }
}