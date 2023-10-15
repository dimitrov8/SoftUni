namespace P04.Recharge.Core
{
    using Contracts;
    using Factories;
    using IO.Contracts;
    using Printer;
    using Recharge.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;

        private readonly Factory factory;
        private readonly ICollection<IWorker> workers;

        private Engine()
        {
            this.factory = new Factory();
            this.workers = new HashSet<IWorker>();
        }

        public Engine(IReader reader, IWriter writer) : this()
        {
            this.reader = reader;
            this.writer = writer;
        }

        public void Run()
        {
            string input; // {WorkerType}
                          // Robot - {Id} {capacity}
                          // Employee - {Id}
            while ((input = this.reader.ReadLine()) != "End")
            {
                string[] args = input.Split();
                try
                {
                    var createdWorker = CreateWorker(args);
                    this.workers.Add(createdWorker);
                }
                catch (ArgumentException argex)
                {
                    this.writer.WriteLine(argex.Message);
                }
            }

            TurnOnAllRobots();
            GetRobotsCurrentBatteryPercentage();
            RechargeRobots();

            EmployeesWork();
            EmployeesSleep();

            WorkersPrinter printer = new WorkersPrinter(this.workers);
            printer.PrintWorkers();
        }

        private IWorker CreateWorker(string[] args)
        {
            IWorker createdWorker = this.factory.CreateWorker(args);
            this.writer.WriteLine(string.Format(Messages.CREATED_WORKER, createdWorker.GetType().Name, createdWorker.Id));

            return createdWorker;
        }

        private void TurnOnAllRobots()
        {
            if (!this.workers.Any(worker => worker is Robot))
                return;
            
            this.writer.WriteLine("--- Robots are turned on ---");
            Random random = new Random();
            int workHours = random.Next(0, 24);
            foreach (IWorker worker in this.workers.Where(worker => worker is Robot))
            {
                Robot robot = (Robot)worker;
                try
                {
                    robot.Work(workHours);
                    this.writer.WriteLine(string.Format(Messages.WORK_MESSAGE, nameof(Robot), robot.Id, workHours));
                }
                catch (ArgumentException argex)
                {
                    this.writer.WriteLine(argex.Message);
                }
            }

            this.writer.WriteLine(null);
        }

        private void GetRobotsCurrentBatteryPercentage()
        {
            if (!this.workers.Any(worker => worker is Robot))
                return;

            this.writer.WriteLine("--- Showing current battery percentage ---");
            foreach (IWorker worker in this.workers.Where(worker => worker is Robot))
            {
                Robot robot = (Robot)worker;
                this.writer.WriteLine(string.Format(Messages.BATTERY_LEFT_MESSAGE, nameof(Robot), robot.Id, robot.CurrentPower));
            }

            this.writer.WriteLine(null);
        }

        private void RechargeRobots()
        {
            if (!this.workers.Any(worker => worker is Robot))
                return;
            
            Console.WriteLine("--- Recharging Robots ---");
            foreach (IWorker worker in this.workers.Where(worker => worker is Robot))
            {
                Robot robot = (Robot)worker;
                robot.Recharge();
                this.writer.WriteLine(string.Format(Messages.RECHARGE_MESSAGE, nameof(Robot), robot.Id, robot.Capacity));
            }
            
            this.writer.WriteLine(null);
        }

        private void EmployeesWork()
        {
            if (!this.workers.Any(worker => worker is Employee))
                return;
            
            this.writer.WriteLine("--- Employees started to work ---");
            Random random = new Random();
            int workHours = random.Next(0, 8);
            foreach (IWorker worker in this.workers.Where(worker => worker is Employee))
            {
                Employee employee = (Employee)worker;
                employee.Work(workHours);
                this.writer.WriteLine(string.Format(Messages.WORK_MESSAGE, nameof(Employee), employee.Id, workHours));
            }

            this.writer.WriteLine(null);
        }

        private void EmployeesSleep()
        {
            if (!this.workers.Any(worker => worker is Employee))
                return;
            
            this.writer.WriteLine("--- Employees are sleeping ---");
            foreach (IWorker worker in this.workers.Where(worker => worker is Employee))
            {
                Employee employee = (Employee)worker;
                employee.Sleep();
            }

            this.writer.WriteLine(null);
        }
    }
}
