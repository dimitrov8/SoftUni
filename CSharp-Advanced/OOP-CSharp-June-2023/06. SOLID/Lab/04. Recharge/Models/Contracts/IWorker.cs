namespace P04.Recharge.Contracts
{
    public interface IWorker
    {
        string Id { get; }

        void Work(int hours);
    }
}