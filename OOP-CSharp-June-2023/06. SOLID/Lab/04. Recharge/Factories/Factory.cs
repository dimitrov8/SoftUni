namespace P04.Recharge.Factories
{
    using System;

    public class Factory
    {
        public Worker CreateWorker(params string[] args)
        {
            Worker worker = args[0] switch
            {
                nameof(Employee) => new Employee(args[1]),
                nameof(Robot) => new Robot(args[1], int.Parse(args[2])),
                _ => throw new ArgumentException("Worker type not supported!")
            };

            return worker;
        }
    }
}