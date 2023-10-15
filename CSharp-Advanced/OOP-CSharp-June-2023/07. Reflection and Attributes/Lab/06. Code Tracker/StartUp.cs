namespace AuthorProblem
{
    public class StartUp
    {
        [Author("Victor")]
        [Author("George")]
        static void Main(string[] args)
        {
            var tracker = new Tracker();
            tracker.PrintMethodsByAuthor();
        }
    }
}