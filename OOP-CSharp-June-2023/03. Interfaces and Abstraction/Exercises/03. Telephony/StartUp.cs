namespace Telephony
{
    using Core;
    using Core.Interfaces;
    using IO;
    using IO.Interfaces;

    public class StartUp
    {
        static void Main(string[] args)
        {
            IWriter writer = new ConsoleWriter();
            IReader reader = new ConsoleReader();

            IEngine engine = new Engine(reader, writer);
            engine.Run();
        }
    }
}