namespace P04.Recharge
{
    using Contracts;
    using Printer;
    using System;

    public class Employee : Worker, ISleeper
    {
        public Employee(string id) : base(id)
        {
        }

        public void Sleep()
        {
            Console.WriteLine(Messages.SLEEP_MESSAGE, GetType().Name, this.Id);
        }
    }
}