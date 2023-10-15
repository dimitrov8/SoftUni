namespace P04.Recharge.Printer
{
    using Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class WorkersPrinter
    {
        private readonly IEnumerable<IWorker> workers;

        public WorkersPrinter(IEnumerable<IWorker> workers)
        {
            this.workers = workers;
        }

        public void PrintWorkers()
        {
            Random random = new Random();

            Console.WriteLine("--- Printing workers ---");
            foreach (IWorker worker in this.workers
                         .OrderByDescending(w
                             => w.GetType().Name)) // Maybe modify the code because it uses reflection and its slower like this
            {
                Console.WriteLine(Messages.PRINT_WORKERS, worker.GetType().Name, worker.Id);
            }
        }
    }
}